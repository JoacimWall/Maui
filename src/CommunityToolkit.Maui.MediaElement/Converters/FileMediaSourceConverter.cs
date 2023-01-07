﻿using System.ComponentModel;
using System.Globalization;

namespace CommunityToolkit.Maui.MediaElement.Converters;

/// <summary>
/// A <see cref="TypeConverter"/> specific to converting a string value to a <see cref="FileMediaSource"/>.
/// </summary>
[TypeConverter(typeof(FileMediaSource))]
public sealed class FileMediaSourceConverter : TypeConverter
{
	/// <inheritdoc/>
	/// <exception cref="InvalidOperationException">Thrown when <paramref name="value"/> is <see langword="null"/> or empty.</exception>
	public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
	{
		var filePath = value.ToString();

		return string.IsNullOrWhiteSpace(filePath)
			? (FileMediaSource)MediaSource.FromFile(filePath)
			: throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(FileMediaSource)}");
	}
}