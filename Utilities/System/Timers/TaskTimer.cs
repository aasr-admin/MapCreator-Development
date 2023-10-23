
using System.Collections.Concurrent;
using System.Diagnostics;

namespace System.Timers
{
	public class TaskTimer
	{
		private static volatile bool _Break;

		private static readonly long _Frequency = Stopwatch.Frequency;

		public static long Ticks => Stopwatch.GetTimestamp();

		public static long TimeStamp => (long)(1000D * Ticks / _Frequency);

		public static ConcurrentDictionary<TaskTimer, long> Timers { get; } = new();

		static TaskTimer()
		{
			AppDomain.CurrentDomain.ProcessExit += OnApplicationExit;
		}

		private static void OnApplicationExit(object? sender, EventArgs e)
		{
			StopAll();
		}

		public static void StopAll()
		{
			_Break = true;

			foreach (var timer in Timers.Keys)
			{
				timer.Stop();
			}

			Timers.Clear();

			_Break = false;
		}

		private CancellationTokenSource _TokenSource = new();

		private readonly Stopwatch _Counter = new();

		private volatile int _Drift, _Elapsed, _Count, _Delay, _Interval = -1, _Repeat = -1;

		public int Elapsed => _Elapsed;

		public float Frequency => Count / (float)_Elapsed;

		public int Count => _Count;

		public int Delay
		{
			get => _Delay;
			set
			{
				value = Math.Max(0, value);

				if (Count == 0)
				{
					_Delay = Delta(_Delay, value);
				}
				else
				{
					_Delay = value;
				}
			}
		}

		public int Interval
		{
			get => _Interval;
			set
			{
				value = Math.Max(-1, value);

				if (Count > 0)
				{
					_Interval = Delta(_Interval, value);
				}
				else
				{
					_Interval = value;
				}
			}
		}

		public int Repeat { get => _Repeat; set => _Repeat = Math.Max(-1, value); }

		public long Last { get; private set; }
		public long Next { get; private set; }

		public bool ReturnContextOnTick { get; set; } = true;

		public Action Callback { get; set; }

		public bool IsRunning => Next > 0 && !_TokenSource.IsCancellationRequested;

		public TaskTimer(int delay, Action callback)
			: this(delay, -1, callback)
		{ }

		public TaskTimer(int delay, int interval, Action callback)
			: this(delay, interval, -1, callback)
		{ }

		public TaskTimer(int delay, int interval, int repeat, Action callback)
		{
			_Delay = Math.Max(0, delay);
			_Interval = Math.Max(-1, interval);
			_Repeat = Math.Max(-1, repeat);

			Callback = callback;
		}

		protected void Reset()
		{
			_Elapsed = 0;
			_Drift = 0;
			_Count = 0;

			OnReset();
		}

		protected virtual void OnReset()
		{
		}

		private void Tick(Task t)
		{
			if (!_Break && t.IsCompleted && IsRunning)
			{
				var sample = _Counter.ElapsedMilliseconds;

				++_Count;

				OnTick();

				Last = TimeStamp;

				if (IsRunning && _Interval >= 0 && (_Repeat < 0 || _Count < _Repeat))
				{
					Run(_Interval, _Counter.ElapsedMilliseconds - sample);

					return;
				}
			}

			Stop();
		}

		protected virtual void OnTick()
		{
			Callback?.Invoke();
		}

		public void Stop()
		{
			if (_Break || Timers.TryRemove(this, out _))
			{
				_TokenSource.Cancel();

				_Counter.Reset();

				_Drift = 0;

				Next = 0;

				OnStop();
			}
		}

		protected virtual void OnStop()
		{
		}

		public void Start()
		{
			if (!IsRunning)
			{
				_Elapsed = 0;
				_Drift = 0;

				_Counter.Start();

				OnStart();

				Run(_Delay, 0);
			}
		}

		protected virtual void OnStart()
		{
		}

		private int Delta(int state, int value)
		{
			if (!IsRunning)
			{
				return value;
			}

			if (state != value)
			{
				var sample = _Counter.ElapsedMilliseconds;
				var next = Next;

				Stop();

				state = value;

				if (state >= 0)
				{
					Run(state + (next - TimeStamp), _Counter.ElapsedMilliseconds - sample);
				}
			}

			return state;
		}

		private async void Run(long delay, long delta)
		{
			delta += _Drift;

			_Drift = 0;

			var sample = _Counter.ElapsedMilliseconds;

			if (_TokenSource.IsCancellationRequested)
			{
				_ = _TokenSource.Token.WaitHandle.WaitOne();

				if (!_TokenSource.TryReset())
				{
					_TokenSource.Dispose();
					_TokenSource = new();
				}
			}

			if (!_Break)
			{
				var now = TimeStamp;
				var exp = Last + delay;

				if (Last > 0 && now - exp > delay)
				{
					delta += now - exp;
				}

				delta += _Counter.ElapsedMilliseconds - sample;

				delay -= delta;

				delay = Math.Clamp(delay, 1L, Int32.MaxValue);

				Timers[this] = Next = now + delay;

				try
				{
					var token = _TokenSource.Token;

					var wait = (int)delay;

					await Task.Delay(wait, token).ContinueWith(t => _Elapsed += wait).ContinueWith(Tick).ConfigureAwait(ReturnContextOnTick);

					var after = _Counter.ElapsedMilliseconds - sample;

					_Drift = Convert.ToInt32(after - delay);

					_Elapsed += _Drift;
				}
				catch (TaskCanceledException)
				{ 
				}

				return;
			}

			Stop();
		}

		public static TaskTimer DelayCall(TimeSpan delay, Action callback)
		{
			var timer = new TaskTimer((int)delay.TotalMilliseconds, callback);

			timer.Start();

			return timer;
		}

		public static TaskTimer DelayCall(TimeSpan delay, TimeSpan interval, Action callback)
		{
			var timer = new TaskTimer((int)delay.TotalMilliseconds, (int)interval.TotalMilliseconds, callback);

			timer.Start();

			return timer;
		}

		public static TaskTimer DelayCall(TimeSpan delay, TimeSpan interval, int count, Action callback)
		{
			var timer = new TaskTimer((int)delay.TotalMilliseconds, (int)interval.TotalMilliseconds, count, callback);

			timer.Start();

			return timer;
		}

		public static TaskTimer DelayCall<T>(TimeSpan delay, Action<T> callback, T state)
		{
			return DelayCall(delay, () => callback?.Invoke(state));
		}

		public static TaskTimer DelayCall<T>(TimeSpan delay, TimeSpan interval, Action<T> callback, T state)
		{
			return DelayCall(delay, interval, () => callback?.Invoke(state));
		}

		public static TaskTimer DelayCall<T>(TimeSpan delay, TimeSpan interval, int count, Action<T> callback, T state)
		{
			return DelayCall(delay, interval, count, () => callback?.Invoke(state));
		}

		public static TaskTimer DelayCall<T1, T2>(TimeSpan delay, Action<T1, T2> callback, T1 state1, T2 state2)
		{
			return DelayCall(delay, o => callback?.Invoke(o.S1, o.S2), (S1: state1, S2: state2));
		}

		public static TaskTimer DelayCall<T1, T2>(TimeSpan delay, TimeSpan interval, Action<T1, T2> callback, T1 state1, T2 state2)
		{
			return DelayCall(delay, interval, o => callback?.Invoke(o.S1, o.S2), (S1: state1, S2: state2));
		}

		public static TaskTimer DelayCall<T1, T2>(TimeSpan delay, TimeSpan interval, int count, Action<T1, T2> callback, T1 state1, T2 state2)
		{
			return DelayCall(delay, interval, count, o => callback?.Invoke(o.S1, o.S2), (S1:state1, S2:state2));
		}

		public static TaskTimer DelayCall<T1, T2, T3>(TimeSpan delay, Action<T1, T2, T3> callback, T1 state1, T2 state2, T3 state3)
		{
			return DelayCall(delay, o => callback?.Invoke(o.S1, o.S2, o.S3), (S1: state1, S2: state2, S3: state3));
		}

		public static TaskTimer DelayCall<T1, T2, T3>(TimeSpan delay, TimeSpan interval, Action<T1, T2, T3> callback, T1 state1, T2 state2, T3 state3)
		{
			return DelayCall(delay, interval, o => callback?.Invoke(o.S1, o.S2, o.S3), (S1: state1, S2: state2, S3: state3));
		}

		public static TaskTimer DelayCall<T1, T2, T3>(TimeSpan delay, TimeSpan interval, int count, Action<T1, T2, T3> callback, T1 state1, T2 state2, T3 state3)
		{
			return DelayCall(delay, interval, count, o => callback?.Invoke(o.S1, o.S2, o.S3), (S1: state1, S2: state2, S3: state3));
		}

		public static TaskTimer DelayCall<T1, T2, T3, T4>(TimeSpan delay, Action<T1, T2, T3, T4> callback, T1 state1, T2 state2, T3 state3, T4 state4)
		{
			return DelayCall(delay, o => callback?.Invoke(o.S1, o.S2, o.S3, o.S4), (S1: state1, S2: state2, S3: state3, S4: state4));
		}

		public static TaskTimer DelayCall<T1, T2, T3, T4>(TimeSpan delay, TimeSpan interval, Action<T1, T2, T3, T4> callback, T1 state1, T2 state2, T3 state3, T4 state4)
		{
			return DelayCall(delay, interval, o => callback?.Invoke(o.S1, o.S2, o.S3, o.S4), (S1: state1, S2: state2, S3: state3, S4: state4));
		}

		public static TaskTimer DelayCall<T1, T2, T3, T4>(TimeSpan delay, TimeSpan interval, int count, Action<T1, T2, T3, T4> callback, T1 state1, T2 state2, T3 state3, T4 state4)
		{
			return DelayCall(delay, interval, count, o => callback?.Invoke(o.S1, o.S2, o.S3, o.S4), (S1: state1, S2: state2, S3: state3, S4: state4));
		}
	}
}
