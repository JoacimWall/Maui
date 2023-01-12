using System.Reflection;
using CoreGraphics;
using UIKit;

namespace CommunityToolkit.Maui.Views;

public partial class Expander
{
	// Let's cache this value to use reflection just once, that way we'll improve performance.
	UIKit.UICollectionViewController? uiCollectionViewController;

	static void ForceUpdateCellSize(Expander expander, CollectionView collectionView, Size size, Point? tapLocation)
	{
		if (tapLocation is null)
		{
			return;
		}
		
		expander.uiCollectionViewController ??= GetControler(collectionView);

		if (expander.uiCollectionViewController?.CollectionView.CollectionViewLayout is UIKit.UICollectionViewFlowLayout layout)
		{
			var cells = layout.CollectionView.VisibleCells.OrderBy(x => x.Frame.Y).ToArray();
			var clickedCell = GetCellByPoint(cells, new CGPoint(tapLocation.Value.X, tapLocation.Value.Y));
			if (clickedCell is null)
			{
				return;
			}

			for (int i = 0; i < cells.Length; i++)
			{
				var cell = cells[i];

				if (i > 0)
				{
					var prevCellFrame = cells[i - 1].Frame;
					cell.Frame = new CGRect(cell.Frame.X, prevCellFrame.Y + prevCellFrame.Height, cell.Frame.Width, cell.Frame.Height);
				}

				if (cell == clickedCell)
				{
					cell.Frame = new CGRect(cell.Frame.X, cell.Frame.Y, cell.Frame.Width, size.Height);
				}
			}
		}
	}

	static UICollectionViewController? GetControler(CollectionView collectionView)
	{
		var handler = collectionView.Handler as Microsoft.Maui.Controls.Handlers.Items.CollectionViewHandler;
		var controller = handler?.GetType().BaseType?.BaseType?.BaseType?.BaseType?.BaseType?.GetProperty("Controller", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetProperty);
		var uiCollectionViewController = controller?.GetValue(handler) as UIKit.UICollectionViewController;
		return uiCollectionViewController;
	}

	static UICollectionViewCell? GetCellByPoint(UICollectionViewCell[] cells, CGPoint point)
	{
		foreach (var cell in cells)
		{
			if (cell.Frame.Contains(point))
			{
				return cell;
			}
		}

		return null;
	}
}