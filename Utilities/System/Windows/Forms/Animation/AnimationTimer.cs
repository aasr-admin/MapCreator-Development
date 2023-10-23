using System.Collections.Concurrent;
using System.Reflection;
using System.Timers;

namespace System.Windows.Forms.Animation
{
	internal sealed class AnimationTimer : TaskTimer
	{
		private static readonly ConcurrentDictionary<MethodInfo, AnimationState> _Active = new();
		private static readonly ConcurrentQueue<AnimationTimer> _Queue = new();

		internal static void Start(AnimationState state)
		{
			var timer = new AnimationTimer(state);

			if (_Active.ContainsKey(state.Animation))
			{
				state.IsQueued = true;

				_Queue.Enqueue(timer);
			}
			else
			{
				timer.Start();
			}
		}

		private readonly AnimationState _State;

		private AnimationTimer(AnimationState state)
			: base(state.Delay, state.Interval, state.FrameCount, state.Invoke)
		{
			_State = state;
		}

		protected override void OnStart()
		{
			_Active[_State.Animation] = _State;

			_State.IsQueued = false;

			base.OnStart();

			Animations.NotifyAnimationStarted(_State);
		}

		protected override void OnStop()
		{
			base.OnStop();

			_State.IsStopRequested = false;

			_ = _Active.TryRemove(_State.Animation, out _);

			Animations.NotifyAnimationStopped(_State);

			if (_Queue.TryDequeue(out var next))
			{
				next.Start();
			}
		}

		protected override void OnReset()
		{
			base.OnReset();

			_State.IsResetRequested = false;

			if (_State.RepeatCount > 0)
			{
				++_State.RepeatIndex;
			}
			else
			{
				_State.RepeatIndex = 0;
			}

			_State.FrameIndex = 0;
			_State.Elapsed = 0;
		}

		protected override void OnTick()
		{
			_State.FrameIndex = Count;
			_State.Elapsed = Elapsed - Delay;

			base.OnTick();

			if (_State.IsResetRequested)
			{
				Reset();
			}

			if (_State.IsStopRequested)
			{
				Stop();
			}
		}
	}
}