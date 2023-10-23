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

		public int FrameIndex => _FrameIndex;

		public int Delay { get; }
		public int Duration { get; }
		public int Interval { get; }

		private volatile int _Elapsed;

		public int Elapsed => _Elapsed;

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

		private volatile bool _IsStopRequested;

		public bool IsStopRequested => _IsStopRequested;

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

		internal AnimationState(Control control, Delegate handler, Delegate? completionCallback, int delayMS, int durationMS, int frameRate)
		{
			Control = control;
			Handler = handler;
			CompletionCallback = completionCallback;

			Delay = Math.Max(delayMS, 0);
			Duration = Math.Max(durationMS, -1);
			FrameRate = Math.Clamp(frameRate, 1, 100);

			Interval = 1000 / FrameRate;
			FrameCount = Duration / Interval;
		}

		public void Stop()
		{
			_IsStopRequested = true;
		}

		internal void Update(int index, int elapsed)
		{
			_FrameIndex = index;
			_Elapsed = elapsed;
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
				Stop();
			}
		}

		private void DynamicInvoke()
		{
			Handler.DynamicInvoke(this);

			if (IsComplete)
			{
				CompletionCallback?.DynamicInvoke(this);
			}
		}
	}

	public class AnimationState<T> : AnimationState
	{
		public T? Object { get; }

		internal AnimationState(Control control, Delegate handler, Delegate? completionCallback, int delayMS, int durationMS, int frameRate, T? @object)
			: base(control, handler, completionCallback, delayMS, durationMS, frameRate)
		{
			Object = @object;
		}
	}
}