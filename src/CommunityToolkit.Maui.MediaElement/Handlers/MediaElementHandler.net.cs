﻿using Microsoft.Maui.Handlers;

namespace CommunityToolkit.Maui.MediaElement;

public partial class MediaElementHandler : ViewHandler<MediaElement, object>
{
	/// <inheritdoc/>
	protected override object CreatePlatformView() => throw new NotImplementedException();

	// Ignoring XML comments for this implementation since it's not used.
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static void MapPosition(object handler, MediaElement mediaElement) => throw new NotImplementedException();
	public static void MapShouldKeepScreenOn(object handler, MediaElement mediaElement) => throw new NotImplementedException();
	public static void MapShouldShowPlaybackControls(object handler, MediaElement mediaElement) => throw new NotImplementedException();
	public static void MapSource(object handler, MediaElement mediaElement) => throw new NotImplementedException();
	public static void MapSpeed(object handler, MediaElement mediaElement) => throw new NotImplementedException();
	public static void MapStatusUpdated(MediaElementHandler handler, MediaElement mediaElement, object? args) => throw new NotImplementedException();
	public static void MapVolume(object handler, MediaElement mediaElement) => throw new NotImplementedException();
	public static void MapPlayRequested(MediaElementHandler handler, MediaElement mediaElement, object? args) => throw new NotImplementedException();
	public static void MapPauseRequested(MediaElementHandler handler, MediaElement mediaElement, object? args) => throw new NotImplementedException();
	public static void MapSeekRequested(MediaElementHandler handler, MediaElement mediaElement, object? args) => throw new NotImplementedException();
	public static void MapStopRequested(MediaElementHandler handler, MediaElement mediaElement, object? args) => throw new NotImplementedException();
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}