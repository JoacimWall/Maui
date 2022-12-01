﻿using Android.Support.V4.Media.Session;
using Android.Widget;
using Com.Google.Android.Exoplayer2;
using Com.Google.Android.Exoplayer2.Audio;
using Com.Google.Android.Exoplayer2.Metadata;
using Com.Google.Android.Exoplayer2.Text;
using Com.Google.Android.Exoplayer2.Trackselection;
using Com.Google.Android.Exoplayer2.UI;
using Com.Google.Android.Exoplayer2.Video;

namespace CommunityToolkit.Maui.MediaElement;

public partial class MediaManager : Java.Lang.Object, IPlayer.IListener
{
	protected StyledPlayerView? playerView;

	// TODO: Make sure we don't need the both and make the StyledPlayerView as android PlatformView
	public (PlatformMediaView platformView, StyledPlayerView playerView) CreatePlatformView()
	{
		ArgumentNullException.ThrowIfNull(mauiContext.Context);
		player = new IExoPlayer.Builder(mauiContext.Context).Build() ?? throw new NullReferenceException();
		player.AddListener(this);
		playerView = new StyledPlayerView(mauiContext.Context)
		{
			Player = player,
			UseController = false,
			ControllerAutoShow = false,
			LayoutParameters = new RelativeLayout.LayoutParams(Android.Views.ViewGroup.LayoutParams.MatchParent, Android.Views.ViewGroup.LayoutParams.MatchParent),
		};
		return (player, playerView);
	}

	protected virtual partial void PlatformPlay(TimeSpan timeSpan)
	{
		// TODO do something with position
		if (player is null)
		{
			return;
		}
		player.Prepare();
		player.Play();
	}

	protected virtual partial void PlatformPause(TimeSpan timeSpan)
	{
		// TODO do something with position
		if (player is null)
		{
			return;
		}

		player.Pause();
	}
	protected virtual partial void PlatformStop(TimeSpan timeSpan)
	{
		// TODO do something with position
		if (player is null || mediaElement is null)
		{
			return;
		}

		// Stops and resets the media player
		player.PlayWhenReady = false;
		player.SeekTo(0);
		player.Stop();

		mediaElement.Position = TimeSpan.Zero;
	}
	protected virtual partial void PlatformUpdateSource()
	{
		var hasSetSource = false;

		if (player is null)
		{
			return;
		}

		if (mediaElement.Source is UriMediaSource)
		{
			string uri = (mediaElement.Source as UriMediaSource)!.Uri!.AbsoluteUri;
			if (!string.IsNullOrWhiteSpace(uri))
			{
				//Com.Google.Android.Exoplayer2.Util.Util.InferContentType()
				//var httpDataSourceFactory = new DefaultHttpDataSource.Factory();
				//var mediaSource = new DashMediaSource.Factory(httpDataSourceFactory)
				//.CreateMediaSource(MediaItem.FromUri(uri));

				//player.SetMediaSource(mediaSource);

				player.SetMediaItem(MediaItem.FromUri(uri));
				player.Prepare();

				hasSetSource = true;
			}
		}
		else if (mediaElement.Source is FileMediaSource)
		{
			string filePath = (mediaElement.Source as FileMediaSource)!.File!;
			if (!string.IsNullOrWhiteSpace(filePath))
			{
				player.SetMediaItem(MediaItem.FromUri(filePath));
				player.Prepare();

				hasSetSource = true;
			}
		}

		if (hasSetSource && mediaElement.AutoPlay)
		{
			player.Play();
		}
	}

	protected virtual partial void PlatformUpdateSpeed()
	{
		if (mediaElement is null || player is null)
		{
			return;
		}

		if (mediaElement.Speed > 0)
		{
			player.SetPlaybackSpeed((float)mediaElement.Speed);
			player.Play();
		}
		else
		{
			player.Pause();
		}
	}

	protected virtual partial void PlatformUpdateShowsPlaybackControls()
	{
		if (mediaElement is null || playerView is null)
		{
			return;
		}

		playerView.UseController = mediaElement.ShowsPlaybackControls;
	}
	protected virtual partial void PlatformUpdatePosition()
	{
		if (mediaElement is null || player is null)
		{
			return;
		}

		if (Math.Abs(player.CurrentPosition - mediaElement.Position.TotalMilliseconds) > 1000)
		{
			player.SeekTo((long)mediaElement.Position.TotalMilliseconds);
		}
	}

	protected virtual partial void PlatformUpdateStatus()
	{
		if (mediaElement is null || player is null)
		{
			return;
		}

		mediaElement.Position = TimeSpan.FromMilliseconds(player.CurrentPosition);
	}

	protected virtual partial void PlatformUpdateVolume()
	{
		if (mediaElement is null || player is null)
		{
			return;
		}

		player.Volume = (float)mediaElement.Volume;
	}


	protected virtual partial void PlatformUpdateIsLooping()
	{
		if (mediaElement is null || player is null)
		{
			return;
		}

		player.RepeatMode = mediaElement.IsLooping ? IPlayer.RepeatModeOne : IPlayer.RepeatModeOff;
	}

	public void OnPlayerStateChanged(bool playWhenReady, int playbackState)
	{
		if (player is null)
		{
			return;
		}

		var previousState = mediaElement.CurrentState;
		MediaElementState newState;

		switch (playbackState)
		{
			case PlaybackStateCompat.StateFastForwarding:
			case PlaybackStateCompat.StateRewinding:
			case PlaybackStateCompat.StateSkippingToNext:
			case PlaybackStateCompat.StateSkippingToPrevious:
			case PlaybackStateCompat.StateSkippingToQueueItem:
			case PlaybackStateCompat.StatePlaying:
				newState = playWhenReady ? MediaElementState.Playing : MediaElementState.Paused;
				break;
			case PlaybackStateCompat.StatePaused:
				newState = MediaElementState.Paused;
				break;
			case PlaybackStateCompat.StateConnecting:
			case PlaybackStateCompat.StateBuffering:
				newState = MediaElementState.Buffering;
				break;
			case PlaybackStateCompat.StateNone:
				newState = MediaElementState.None;
				break;
			case PlaybackStateCompat.StateStopped:
				newState = MediaElementState.Stopped;
				break;
			case PlaybackStateCompat.StateError:
				newState = MediaElementState.Failed;
				break;
			default:
				newState = MediaElementState.None;
				break;
		}

		if (newState != previousState)
		{
			mediaElement.CurrentStateChanged(new MediaStateChangedEventArgs(previousState, newState));
		}

		if (playbackState == IPlayer.StateReady)
		{
			mediaElement.Duration = TimeSpan.FromMilliseconds(
				player.Duration < 0 ? 0 : player.Duration);
		}
	}

	public void OnPlayerError(PlaybackException? error)
	{
		if (mediaElement is null)
		{
			return;
		}

		// TODO add more detail to error message?
		mediaElement?.MediaFailed(new MediaFailedEventArgs(error?.Message ?? ""));
	}

	public void OnSeekProcessed()
	{
		mediaElement?.SeekCompleted();
	}

	#region IPlayer.IListener implementation method stubs
	public void OnAudioAttributesChanged(AudioAttributes? audioAttributes) { }
	public void OnAudioSessionIdChanged(int audioSessionId) { }
	public void OnAvailableCommandsChanged(IPlayer.Commands? availableCommands) { }
	public void OnCues(CueGroup? cueGroup) { }
	public void OnCues(List<Cue> cues) { }
	public void OnDeviceInfoChanged(Com.Google.Android.Exoplayer2.DeviceInfo? deviceInfo) { }
	public void OnDeviceVolumeChanged(int volume, bool muted) { }
	public void OnEvents(IPlayer? player, IPlayer.Events? events) { }
	public void OnIsLoadingChanged(bool isLoading) { }
	public void OnIsPlayingChanged(bool isPlaying) { }
	public void OnLoadingChanged(bool isLoading) { }
	public void OnMaxSeekToPreviousPositionChanged(long maxSeekToPreviousPositionMs) { }
	public void OnMediaItemTransition(MediaItem? mediaItem, int transition) { }
	public void OnMediaMetadataChanged(MediaMetadata? mediaMetadata) { }
	public void OnMetadata(Metadata? metadata) { }
	public void OnPlaybackParametersChanged(PlaybackParameters? playbackParameters) { }
	public void OnPlaybackStateChanged(int playbackState) { }
	public void OnPlaybackSuppressionReasonChanged(int playbackSuppressionReason) { }
	public void OnPlayerErrorChanged(PlaybackException? error) { }
	public void OnPlaylistMetadataChanged(MediaMetadata? mediaMetadata) { }
	public void OnPlayWhenReadyChanged(bool playWhenReady, int reason) { }
	public void OnPositionDiscontinuity(int reason) { }
	public void OnPositionDiscontinuity(IPlayer.PositionInfo oldPosition, IPlayer.PositionInfo newPosition, int reason) { }
	public void OnRenderedFirstFrame() { }
	public void OnRepeatModeChanged(int repeatMode) { }
	public void OnSeekBackIncrementChanged(long seekBackIncrementMs) { }
	public void OnSeekForwardIncrementChanged(long seekForwardIncrementMs) { }
	public void OnShuffleModeEnabledChanged(bool shuffleModeEnabled) { }
	public void OnSkipSilenceEnabledChanged(bool skipSilenceEnabled) { }
	public void OnSurfaceSizeChanged(int width, int height) { }
	public void OnTimelineChanged(Timeline? timeline, int reason) { }
	public void OnTracksChanged(Tracks? tracks) { }
	public void OnTrackSelectionParametersChanged(TrackSelectionParameters? trackSelectionParameters) { }
	public void OnVideoSizeChanged(VideoSize? videoSize) { }
	public void OnVolumeChanged(VideoSize? videoSize) { }
	#endregion
}
