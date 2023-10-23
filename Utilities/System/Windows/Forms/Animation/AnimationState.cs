using System.Reflection;

namespace System.Windows.Forms.Animation
{
	public class AnimationState
	{
		public Control Control { get; }

		internal Delegate Handler { get; }

		internal Delegate? CompletionCallback { get; }

		public MethodInfo Animation => Handler.Method;

		public int FrameRate { get; }
		public int FrameCount { get; }

		private volatile int _FrameIndex;

		public int FrameIndex { get => _FrameIndex; internal set => _FrameIndex = value; }

		public int RepeatCount { get; }

		private volatile int _RepeatIndex;

		public int RepeatIndex { get => _RepeatIndex; internal set => _RepeatIndex = value; }

		public int Delay { get; }
		public int Duration { get; }
		public int Interval { get; }

		private volatile int _Elapsed;

		public int Elapsed { get => _Elapsed; internal set => _Elapsed = value; }

		public float Frequency => _FrameIndex / (float)_Elapsed;

		public float Progress
		{
			get
			{
				if (_IsStopRequested)
				{
					return 1f;
				}

				if (FrameCount > 0)
				{
					return Math.Clamp(_FrameIndex / (float)FrameCount, 0f, 1f);
				}

				if (Duration > 0)
				{
					return Math.Clamp(_Elapsed / (float)Duration, 0f, 1f);
				}

				return Frequency;
			}
		}

		public bool IsComplete => Progress >= 1f;

		internal bool IsValidHandle
		{
			get
			{
				if (!Control.IsHandleCreated)
				{
					return false;
				}

				var form = Control.FindForm();

				if (form == null || form.WindowState == FormWindowState.Minimized || form.WindowState == FormWindowState.Maximized)
				{
					return false;
				}

				return true;
			}
		}

		private volatile bool _IsQueued;

		public bool IsQueued { get => _IsQueued; internal set => _IsQueued = value; }

		private volatile bool _IsStopRequested;

		public bool IsStopRequested { get => _IsStopRequested; internal set => _IsStopRequested = value; }

		private volatile bool _IsResetRequested;

		public bool IsResetRequested { get => _IsResetRequested; internal set => _IsResetRequested = value; }

		internal AnimationState(Control control, Delegate handler, Delegate? completionCallback, int delayMS, int durationMS, int count, int frameRate)
		{
			Control = control;
			Handler = handler;
			CompletionCallback = completionCallback;

			Delay = Math.Max(delayMS, 0);
			Duration = Math.Max(durationMS, -1);

			RepeatCount = Math.Max(count - 1, 0);

			FrameRate = Math.Clamp(frameRate, 1, 100);

			Interval = 1000 / FrameRate;
			FrameCount = Duration / Interval;
		}

		public void Stop()
		{
			_IsStopRequested = true;
		}

		public void Reset()
		{
			_IsResetRequested = true;
		}

		internal void Invoke()
		{
			if (Control.IsDisposed || Control.Disposing)
			{
				Stop();
				return;
			}

			if (IsValidHandle && Control.InvokeRequired)
			{
				Control.Invoke(DynamicInvoke);
			}
			else
			{
				DynamicInvoke();
			}

			if (IsComplete)
			{
				if (!_IsStopRequested && (RepeatCount < 0 || (RepeatCount > 0 && _RepeatIndex < RepeatCount)))
				{
					Reset();
				}
				else
				{
					Stop();
				}
			}
		}

		private void DynamicInvoke()
		{
			Handler.DynamicInvoke(this);

			if (IsComplete && !_IsResetRequested)
			{
				CompletionCallback?.DynamicInvoke(this);
			}
		}
	}

	public class AnimationState<T> : AnimationState
	{
		public T? Object { get; }

		internal AnimationState(Control control, Delegate handler, Delegate? completionCallback, int delayMS, int durationMS, int count, int frameRate, T? @object)
			: base(control, handler, completionCallback, delayMS, durationMS, count, frameRate)
		{
			Object = @object;
		}
	}
}