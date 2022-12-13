﻿#if !(ANDROID || IOS || WINDOWS || MACCATALYST)
global using PlatformMediaView = System.Object;
#elif ANDROID
global using PlatformMediaView = Com.Google.Android.Exoplayer2.IExoPlayer;
#elif IOS || MACCATALYST
global using PlatformMediaView = AVFoundation.AVPlayer;
#elif WINDOWS
global using PlatformMediaView = Microsoft.UI.Xaml.Controls.MediaPlayerElement;
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CommunityToolkit.Maui.MediaElement;

public partial class MediaManager
{
	readonly IMauiContext mauiContext;
	readonly IMediaElement mediaElement;
	PlatformMediaView? player;

	public MediaManager(IMauiContext context, IMediaElement mediaElement)
	{
		this.mauiContext = context;
		this.mediaElement = mediaElement;

		Logger = mauiContext.Services.GetService<ILoggerFactory>()?
			.CreateLogger(nameof(MediaManager));
	}

	public ILogger? Logger { get; }

	public void Play()
	{
		PlatformPlay();
	}

	public void Pause()
	{
		PlatformPause();
	}

	public void Stop()
	{
		PlatformStop();
	}

	public void UpdateSource()
	{
		PlatformUpdateSource();
	}

	public void UpdateSpeed()
	{
		PlatformUpdateSpeed();
	}

	public void UpdateShowsPlaybackControls()
	{
		PlatformUpdateShowsPlaybackControls();
	}

	public void UpdatePosition()
	{
		PlatformUpdatePosition();
	}

	public void UpdateStatus()
	{
		PlatformUpdateStatus();
	}

	public void UpdateVolume()
	{
		PlatformUpdateVolume();
	}

	public void UpdateKeepScreenOn()
	{
		PlatformUpdateKeepScreenOn();
	}

	public void UpdateIsLooping()
	{
		PlatformUpdateIsLooping();
	}

	protected virtual partial void PlatformPlay();
	protected virtual partial void PlatformPause();
	protected virtual partial void PlatformStop();
	protected virtual partial void PlatformUpdateSource();
	protected virtual partial void PlatformUpdateSpeed();
	protected virtual partial void PlatformUpdateShowsPlaybackControls();
	protected virtual partial void PlatformUpdatePosition();
	protected virtual partial void PlatformUpdateStatus();
	protected virtual partial void PlatformUpdateVolume();
	protected virtual partial void PlatformUpdateKeepScreenOn();
	protected virtual partial void PlatformUpdateIsLooping();
}


#if !(WINDOWS || ANDROID || IOS || MACCATALYST)
partial class MediaManager
{
	protected virtual partial void PlatformPlay()
	{

	}

	protected virtual partial void PlatformPause()
	{

	}

	protected virtual partial void PlatformStop()
	{

	}

	protected virtual partial void PlatformUpdateSource()
	{

	}

	protected virtual partial void PlatformUpdateSpeed()
	{

	}

	protected virtual partial void PlatformUpdateShowsPlaybackControls()
	{

	}

	protected virtual partial void PlatformUpdatePosition()
	{

	}

	protected virtual partial void PlatformUpdateStatus()
	{

	}

	protected virtual partial void PlatformUpdateVolume()
	{

	}

	protected virtual partial void PlatformUpdateKeepScreenOn()
	{

	}

	protected virtual partial void PlatformUpdateIsLooping()
	{

	}
}
#endif