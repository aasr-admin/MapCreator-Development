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

			_ = _Active.TryRemove(_State.Animation, out _);

			Animations.NotifyAnimationStopped(_State);

			if (_Queue.TryDequeue(out var next))
			{
				next.Start();
			}
		}

		protected override void OnTick()
		{
			_State.Update(Count, Elapsed - Delay);

			base.OnTick();

			if (_State.IsStopRequested)
			{
				Stop();
			}
		}
	}
}