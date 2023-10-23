﻿using System.Collections.Concurrent;

namespace System.Windows.Forms.Animation
{
	public delegate void AnimationHandler(AnimationState state);

	public delegate void AnimationHandler<T>(AnimationState<T> state);

	public static class Animations
	{
		private static readonly Dictionary<Control, HashSet<AnimationState>> _ActiveAnimations = new();

		public static int DefaultFrameRate { get; set; } = 60;

		public static int DefaultDelay { get; set; } = 0;

		public static int DefaultDuration { get; set; } = 100;

		public static event EventHandler<AnimationState>? AnimationStarted;
		public static event EventHandler<AnimationState>? AnimationStopped;

		internal static void NotifyAnimationStarted(AnimationState state)
		{
			if (!_ActiveAnimations.TryGetValue(state.Control, out var states))
			{
				_ActiveAnimations[state.Control] = states = new HashSet<AnimationState>();
			}

			if (states.Add(state))
			{
				AnimationStarted?.Invoke(state.Control, state);
			}
			else if (states.Count == 0)
			{
				_ = _ActiveAnimations.Remove(state.Control);
			}
		}

		internal static void NotifyAnimationStopped(AnimationState state)
		{
			if (!_ActiveAnimations.TryGetValue(state.Control, out var states))
			{
				return;
			}

			if (states.Remove(state))
			{
				if (states.Count == 0)
				{
					_ = _ActiveAnimations.Remove(state.Control);
				}

				AnimationStopped?.Invoke(state.Control, state);
			}
		}

		public static bool IsAnimating(this Control control)
		{
			return _ActiveAnimations.ContainsKey(control);
		}

		public static IReadOnlySet<AnimationState>? GetActiveAnimations(this Control control)
		{
			_ActiveAnimations.TryGetValue(control, out var states);

			return states;
		}

		public static int StopActiveAnimations(this Control control)
		{
			var count = 0;

			foreach (var states in _ActiveAnimations.Values)
			{
				foreach (var state in states)
				{
					state.Stop();

					++count;
				}
			}

			return count;
		}

		public static AnimationState Animate(this Control control, AnimationHandler animation, AnimationHandler? completed = null)
		{
			return Animate(control, DefaultDelay, animation, completed);
		}

		public static AnimationState Animate(this Control control, int delayMS, AnimationHandler animation, AnimationHandler? completed = null)
		{
			return Animate(control, delayMS, DefaultDuration, animation, completed);
		}

		public static AnimationState Animate(this Control control, int delayMS, int durationMS, AnimationHandler animation, AnimationHandler? completed = null)
		{
			return Animate(control, delayMS, durationMS, DefaultFrameRate, animation);
		}

		public static AnimationState Animate(this Control control, int delayMS, int durationMS, int frameRate, AnimationHandler animation, AnimationHandler? completed = null)
		{
			var state = new AnimationState(control, animation, completed, delayMS, durationMS, frameRate);

			try
			{
				return state;
			}
			finally
			{
				AnimationTimer.Start(state);
			}
		}

		public static AnimationState<T> Animate<T>(this Control control, AnimationHandler<T> animation, T? @object, AnimationHandler<T>? completed = null)
		{
			return Animate(control, DefaultDelay, animation, @object, completed);
		}

		public static AnimationState<T> Animate<T>(this Control control, int delayMS, AnimationHandler<T> animation, T? @object, AnimationHandler<T>? completed = null)
		{
			return Animate(control, delayMS, DefaultDuration, animation, @object, completed);
		}

		public static AnimationState<T> Animate<T>(this Control control, int delayMS, int durationMS, AnimationHandler<T> animation, T? @object, AnimationHandler<T>? completed = null)
		{
			return Animate(control, delayMS, durationMS, DefaultFrameRate, animation, @object, completed);
		}

		public static AnimationState<T> Animate<T>(this Control control, int delayMS, int durationMS, int frameRate, AnimationHandler<T> animation, T? @object, AnimationHandler<T>? completed = null)
		{
			var state = new AnimationState<T>(control, animation, completed, delayMS, durationMS, frameRate, @object);

			try
			{
				return state;
			}
			finally
			{
				AnimationTimer.Start(state);
			}
		}

		#region Resize

		public static AnimationState<Size> AnimateResize(this Control control, Size targetSize, AnimationHandler<Size>? completed = null)
		{
			return AnimateResize(control, targetSize, DefaultDelay, completed);
		}

		public static AnimationState<Size> AnimateResize(this Control control, Size targetSize, int delayMS, AnimationHandler<Size>? completed = null)
		{
			return AnimateResize(control, targetSize, delayMS, DefaultDuration, completed);
		}

		public static AnimationState<Size> AnimateResize(this Control control, Size targetSize, int delayMS, int durationMS, AnimationHandler<Size>? completed = null)
		{
			if (control.MaximumSize.Width > 0)
			{
				targetSize.Width = Math.Clamp(targetSize.Width, control.MinimumSize.Width, control.MaximumSize.Width);
			}
			else
			{
				targetSize.Width = Math.Max(targetSize.Width, control.MinimumSize.Width);
			}

			if (control.MaximumSize.Height > 0)
			{
				targetSize.Height = Math.Clamp(targetSize.Height, control.MinimumSize.Height, control.MaximumSize.Height);
			}
			else
			{
				targetSize.Height = Math.Max(targetSize.Height, control.MinimumSize.Height);
			}

			return Animate(control, delayMS, durationMS, HandleResizeAnimation, targetSize, completed);
		}

		private static void HandleResizeAnimation(AnimationState<Size> state)
		{
			var currentSize = state.Control.Size;
			var targetSize = state.Object;

			if (currentSize == targetSize)
			{
				state.Stop();
				return;
			}

			if (currentSize.Width != targetSize.Width)
			{
				currentSize.Width += (int)((targetSize.Width - currentSize.Width) * state.Progress);
			}

			if (currentSize.Height != targetSize.Height)
			{
				currentSize.Height += (int)((targetSize.Height - currentSize.Height) * state.Progress);
			}

			state.Control.Size = currentSize;

			if (state.Control.Size != currentSize || currentSize == targetSize)
			{
				state.Stop();
			}
		}

		#endregion

		#region Move

		public static AnimationState<Point> AnimateMove(this Control control, Point targetLocation, AnimationHandler<Point>? completed = null)
		{
			return AnimateMove(control, targetLocation, DefaultDelay, completed);
		}

		public static AnimationState<Point> AnimateMove(this Control control, Point targetLocation, int delayMS, AnimationHandler<Point>? completed = null)
		{
			return AnimateMove(control, targetLocation, delayMS, DefaultDuration, completed);
		}

		public static AnimationState<Point> AnimateMove(this Control control, Point targetLocation, int delayMS, int durationMS, AnimationHandler<Point>? completed = null)
		{
			if (control is Form form)
			{
				var screen = Screen.FromControl(form);

				targetLocation.X = Math.Clamp(targetLocation.X, screen.WorkingArea.Left, screen.WorkingArea.Right - form.Width);
				targetLocation.Y = Math.Clamp(targetLocation.Y, screen.WorkingArea.Top, screen.WorkingArea.Bottom - form.Height);
			}
			else if (control.Parent is Control parentControl)
			{
				targetLocation.X = Math.Clamp(targetLocation.X, parentControl.Bounds.Left, parentControl.Bounds.Right - control.Width);
				targetLocation.Y = Math.Clamp(targetLocation.Y, parentControl.Bounds.Top, parentControl.Bounds.Bottom - control.Height);
			}

			return Animate(control, delayMS, durationMS, HandleMoveAnimation, targetLocation, completed);
		}

		private static void HandleMoveAnimation(AnimationState<Point> state)
		{
			var currentLocation = state.Control.Location;
			var targetLocation = state.Object;

			if (currentLocation == targetLocation)
			{
				state.Stop();
				return;
			}

			if (currentLocation.X != targetLocation.X)
			{
				currentLocation.X += (int)((targetLocation.X - currentLocation.X) * state.Progress);
			}

			if (currentLocation.Y != targetLocation.Y)
			{
				currentLocation.Y += (int)((targetLocation.Y - currentLocation.Y) * state.Progress);
			}

			state.Control.Location = currentLocation;

			if (state.Control.Location != currentLocation || currentLocation == targetLocation)
			{
				state.Stop();
			}
		}

		#endregion

		#region Fade

		public static AnimationState<Color> AnimateFadeIn(this Control control, Color overlayColor, AnimationHandler<Color>? completed = null)
		{
			return AnimateFadeIn(control, overlayColor, DefaultDelay, completed);
		}

		public static AnimationState<Color> AnimateFadeIn(this Control control, Color overlayColor, int delayMS, AnimationHandler<Color>? completed = null)
		{
			return AnimateFadeIn(control, overlayColor, delayMS, DefaultDuration, completed);
		}
		
		public static AnimationState<Color> AnimateFadeIn(this Control control, Color overlayColor, int delayMS, int durationMS, AnimationHandler<Color>? completed = null)
		{
			overlayColor = Color.FromArgb(0, overlayColor);

			return Animate(control, delayMS, durationMS, HandleFadeAnimation, overlayColor, completed);
		}

		public static AnimationState<Color> AnimateFadeOut(this Control control, Color overlayColor, AnimationHandler<Color>? completed = null)
		{
			return AnimateFadeOut(control, overlayColor, DefaultDelay, completed);
		}

		public static AnimationState<Color> AnimateFadeOut(this Control control, Color overlayColor, int delayMS, AnimationHandler<Color>? completed = null)
		{
			return AnimateFadeOut(control, overlayColor, delayMS, DefaultDuration, completed);
		}

		public static AnimationState<Color> AnimateFadeOut(this Control control, Color overlayColor, int delayMS, int durationMS, AnimationHandler<Color>? completed = null)
		{
			overlayColor = Color.FromArgb(255, overlayColor);

			return Animate(control, delayMS, durationMS, HandleFadeAnimation, overlayColor, completed);
		}

		private static void HandleFadeAnimation(AnimationState<Color> state)
		{
			var f = state.Progress;

			if (state.Object.A == 0)
			{
				f = 1f - f;
			}

			using var g = state.Control.CreateGraphics();

			using var b = new SolidBrush(Color.FromArgb((int)(255 * f), state.Object));

			g.FillRectangle(b, state.Control.DisplayRectangle);
		}

		#endregion
	}
}