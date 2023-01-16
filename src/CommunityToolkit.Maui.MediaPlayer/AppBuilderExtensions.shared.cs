﻿namespace CommunityToolkit.Maui.MediaPlayer;

/// <summary>
/// This class contains MediaPlayer's <see cref="MauiAppBuilder"/> extensions.
/// </summary>
public static class AppBuilderExtensions
{
	/// <summary>
	/// Initializes the .NET MAUI Community Toolkit MediaPlayer Library
	/// </summary>
	/// <param name="builder"><see cref="MauiAppBuilder"/> generated by <see cref="MauiApp"/>.</param>
	/// <returns><see cref="MauiAppBuilder"/> initialized for <see cref="MediaPlayer"/>.</returns>
	public static MauiAppBuilder UseMauiCommunityToolkitMediaPlayer(this MauiAppBuilder builder)
	{
		builder.ConfigureMauiHandlers(h =>
		{
			h.AddHandler<Views.MediaPlayer, MediaPlayerHandler>();
		});

		return builder;
	}
}