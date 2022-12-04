﻿namespace CommunityToolkit.Maui.MediaElement;

/// <summary>
/// With MediaElement you can play multimedia inside of your app.
/// </summary>
public interface IMediaElement : IView
{
	/// <summary>
	/// Gets or sets whether the media should start playing as soon as it's loaded. Default is <see langword="false"/>.
	/// </summary>
	bool AutoPlay { get; set; }

	/// <summary>
	/// The current state of the <see cref="MediaElement"/>.
	/// </summary>
	MediaElementState CurrentState { get; }

	/// <summary>
	/// Occurs when <see cref="CurrentState"/> changes.
	/// </summary>
	/// <param name="newState">The new state the <see cref="MediaElement"/> transitioned to.</param>
	void CurrentStateChanged(MediaElementState newState);

	/// <summary>
	/// The total duration of the loaded media.
	/// </summary>
	/// <remarks>Might not be available for some types, like live streams.</remarks>
	TimeSpan Duration { get; set; }

	/// <summary>
	/// Occurs when the media has ended playing successfully.
	/// </summary>
	/// <remarks>This does not trigger when the media has failed during playback.</remarks>
	void MediaEnded();

	/// <summary>
	/// Occurs when the media has failed loading.
	/// </summary>
	/// <param name="args">Event arguments containing extra information about this event.</param>
	void MediaFailed(MediaFailedEventArgs args);

	/// <summary>
	/// Occurs when the media has been loaded and is ready to play.
	/// </summary>
	void MediaOpened();

	/// <summary>
	/// Pauses the currently playing media.
	/// </summary>
	void Pause();

	/// <summary>
	/// Starts playing the loaded media.
	/// </summary>
	void Play();

	/// <summary>
	/// The current position of the playing media.
	/// </summary>
	TimeSpan Position { get; set; }

	/// <summary>
	/// Fired when a seek action has been completed.
	/// </summary>
	void SeekCompleted();

	/// <summary>
	/// Gets or sets whether to show the playback controls.
	/// </summary>
	bool ShowsPlaybackControls { get; set; }

	/// <summary>
	/// Gets or sets if the video will play when reaches the end.
	/// </summary>
	bool IsLooping { get; set; }

	/// <summary>
	/// Gets or sets if media playback will prevent the device display from going to sleep.
	/// If media is paused, stopped or has completed playing, the display will turn off.
	/// </summary>
	/// <remarks>Only works on mobile devices.</remarks>
	public bool KeepScreenOn { get; set; }

	/// <summary>
	/// The source of the media to play.
	/// </summary>
	MediaSource? Source { get; set; }

	/// <summary>
	/// Gets or sets the speed with which the media should be played.
	/// </summary>
	/// <remarks>A value of 1 means normal speed.
	/// Anything more than 1 is faster speed, anything less than 1 is slower speed.</remarks>
	double Speed { get; set; }

	/// <summary>
	/// Stops playing the currently playing media and resets the <see cref="Position"/>.
	/// </summary>
	void Stop();

	/// <summary>
	/// The height (in pixels) of the loaded media in pixels.
	/// </summary>
	/// <remarks>Not reported for non-visual media.</remarks>
	int VideoHeight { get; }

	/// <summary>
	/// The width (in pixels) of the loaded media in pixels.
	/// </summary>
	/// <remarks>Not reported for non-visual media.</remarks>
	int VideoWidth { get; }

	/// <summary>
	/// Gets or sets the volume of the audio for the media.
	/// </summary>
	/// <remarks>A value of 1 means full volume, 0 is silence.</remarks>
	double Volume { get; set; }
}