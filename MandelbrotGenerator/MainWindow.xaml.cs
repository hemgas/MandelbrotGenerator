using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MandelbrotGenerator
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Image_Loaded(Object sender, RoutedEventArgs e)
		{
		}

		private void RenderImage()
		{
			var sizeX = (Int32) Border.ActualWidth;
			var sizeY = (Int32) Border.ActualHeight;
			if (sizeX <= 0 || sizeY <= 0) {
				return;
			}
			var m = MandelbrotImage.Create(
										pixelWidth: sizeX,
										pixelHeight: sizeY,
										center: new Complex(-0.7, 0),
										zoomFactor: sizeX / 3.0,
										maxIterations: 32,
										escapeRadius: 10000
									);
			var bitmap = BitmapSource.Create(
										pixelWidth: sizeX,
										pixelHeight: sizeY,
										dpiX: 96,
										dpiY: 96,
										pixelFormat: PixelFormats.Gray8,
										palette: null,
										pixels: m,
										stride: sizeX
									);
			Image.Source = bitmap;
		}

		private String ValueOf(DependencyProperty dp)
		{
			try {
				var value = Image.GetValue(dp);
				return value?.ToString() ?? "-";
			}
			catch {
				return ".";
			}
		}

		#region 1. EventArgs

		private String ShowBase(String memberName) => 
			$"### {memberName}: {ValueOf(HeightProperty)} {ValueOf(ActualHeightProperty)}";

		private void T(Object sender, EventArgs e, [CallerMemberName] String memberName = "") => 
			Trace.WriteLine(ShowBase(memberName));

		private void Window_Activated(Object sender, EventArgs e) => T(sender, e);
		private void Window_Closed(Object sender, EventArgs e) => T(sender, e);

		private void Window_ContentRendered(Object sender, EventArgs e)
		{
			T(sender, e);
			RenderImage();
		}

		private void Window_Deactivated(Object sender, EventArgs e) => T(sender, e);
		private void Window_Initialized(Object sender, EventArgs e) => T(sender, e);
		private void Window_LayoutUpdated(Object sender, EventArgs e) => T(sender, e);
		private void Window_LocationChanged(Object sender, EventArgs e) => T(sender, e);
		private void Window_StateChanged(Object sender, EventArgs e) => T(sender, e);
		private void Window_SourceInitialized(Object sender, EventArgs e) => T(sender, e);

		#region 1.1 RoutedEventArgs

		private void Window_GotFocus(Object sender, RoutedEventArgs e) => T(sender, e);
		private void Window_LostFocus(Object sender, RoutedEventArgs e) => T(sender, e);
		private void Window_Loaded(Object sender, RoutedEventArgs e) => T(sender, e);
		private void Window_Unloaded(Object sender, RoutedEventArgs e) => T(sender, e);

		#region 1.1.1 GiveFeedbackEventArgs
		private void Window_GiveFeedback(Object sender, GiveFeedbackEventArgs e) => T(sender, e);
		private void Window_PreviewGiveFeedback(Object sender, GiveFeedbackEventArgs e) => T(sender, e);
		#endregion

		#region 1.1.2 DragEventArgs
		private void Window_DragEnter(Object sender, DragEventArgs e) => T(sender, e);
		private void Window_DragLeave(Object sender, DragEventArgs e) => T(sender, e);
		private void Window_DragOver(Object sender, DragEventArgs e) => T(sender, e);
		private void Window_Drop(Object sender, DragEventArgs e) => T(sender, e);
		private void Window_PreviewDragEnter(Object sender, DragEventArgs e) => T(sender, e);
		private void Window_PreviewDragLeave(Object sender, DragEventArgs e) => T(sender, e);
		private void Window_PreviewDragOver(Object sender, DragEventArgs e) => T(sender, e);
		private void Window_PreviewDrop(Object sender, DragEventArgs e) => T(sender, e);
		#endregion

		#region 1.1.3 ContextMenuEventArgs
		private void Window_ContextMenuClosing(Object sender, ContextMenuEventArgs e) => T(sender, e);
		private void Window_ContextMenuOpening(Object sender, ContextMenuEventArgs e) => T(sender, e);
		#endregion

		#region 1.1.4 InputEventArgs

		#region 1.1.4.1 MouseEventArgs

		private String ShowMouseEventArgs(MouseEventArgs e, String memberName)
		{
			var pos = e.GetPosition(this);
			return	ShowBase(memberName) +
					$" ({pos.X}/{pos.Y})" +
					Show(e.LeftButton, 'L') +
					Show(e.MiddleButton, 'M') +
					Show(e.RightButton, 'R');

			String Show(MouseButtonState button, Char v) =>
				$" {(button == MouseButtonState.Pressed ? v : Char.ToLower(v))}";
		}

		private void T(Object sender, MouseEventArgs e, [CallerMemberName] String memberName = "") =>
			Trace.WriteLine(ShowMouseEventArgs(e, memberName));

		private void Window_GotMouseCapture(Object sender, MouseEventArgs e) => T(sender, e);
		private void Window_LostMouseCapture(Object sender, MouseEventArgs e) => T(sender, e);
		private void Window_MouseEnter(Object sender, MouseEventArgs e) => T(sender, e);
		private void Window_MouseLeave(Object sender, MouseEventArgs e) => T(sender, e);
		private void Window_MouseMove(Object sender, MouseEventArgs e) => T(sender, e);
		private void Window_PreviewMouseMove(Object sender, MouseEventArgs e) => T(sender, e);

		#region 1.1.4.1 MouseButtonEventArgs
		private void T(Object sender, MouseButtonEventArgs e, [CallerMemberName] String memberName = "") =>
			Trace.WriteLine(ShowBase(memberName) + $" {e.ChangedButton}: {e.ButtonState} {e.ClickCount}x");

		private void Window_MouseDoubleClick(Object sender, MouseButtonEventArgs e) => T(sender, e);
		private void Window_MouseDown(Object sender, MouseButtonEventArgs e) => T(sender, e);
		private void Window_MouseLeftButtonDown(Object sender, MouseButtonEventArgs e) => T(sender, e);
		private void Window_MouseLeftButtonUp(Object sender, MouseButtonEventArgs e) => T(sender, e);
		private void Window_MouseRightButtonDown(Object sender, MouseButtonEventArgs e) => T(sender, e);
		private void Window_MouseRightButtonUp(Object sender, MouseButtonEventArgs e) => T(sender, e);
		private void Window_MouseUp(Object sender, MouseButtonEventArgs e) => T(sender, e);
		private void Window_PreviewMouseDoubleClick(Object sender, MouseButtonEventArgs e) => T(sender, e);
		private void Window_PreviewMouseDown(Object sender, MouseButtonEventArgs e) => T(sender, e);
		private void Window_PreviewMouseLeftButtonDown(Object sender, MouseButtonEventArgs e) => T(sender, e);
		private void Window_PreviewMouseLeftButtonUp(Object sender, MouseButtonEventArgs e) => T(sender, e);
		private void Window_PreviewMouseRightButtonDown(Object sender, MouseButtonEventArgs e) => T(sender, e);
		private void Window_PreviewMouseRightButtonUp(Object sender, MouseButtonEventArgs e) => T(sender, e);
		private void Window_PreviewMouseUp(Object sender, MouseButtonEventArgs e) => T(sender, e);
		#endregion

		#region 1.1.4.2 MouseWheelEventArgs
		private void T(Object sender, MouseWheelEventArgs e, [CallerMemberName] String memberName = "") =>
			Trace.WriteLine(ShowMouseEventArgs(e, memberName) + $" Delta: {e.Delta}");

		private void Window_MouseWheel(Object sender, MouseWheelEventArgs e) => T(sender, e);
		private void Window_PreviewMouseWheel(Object sender, MouseWheelEventArgs e) => T(sender, e);
		#endregion

		#region 1.1.4.3 QueryCursorEventArgs
		private void Window_QueryCursor(Object sender, QueryCursorEventArgs e) => T(sender, e);
		#endregion

		#endregion

		#region 1.1.4.2 KeyboardEventArgs

		#region 1.1.4.2.1 KeyEventArgs
		private void Window_KeyDown(Object sender, KeyEventArgs e) => T(sender, e);
		private void Window_KeyUp(Object sender, KeyEventArgs e) => T(sender, e);
		private void Window_PreviewKeyDown(Object sender, KeyEventArgs e) => T(sender, e);
		private void Window_PreviewKeyUp(Object sender, KeyEventArgs e) => T(sender, e);
		#endregion

		#region 1.1.4.2.2 KeyboardFocusChangedEventArgs
		private void Window_GotKeyboardFocus(Object sender, KeyboardFocusChangedEventArgs e) => T(sender, e);
		private void Window_LostKeyboardFocus(Object sender, KeyboardFocusChangedEventArgs e) => T(sender, e);
		private void Window_PreviewGotKeyboardFocus(Object sender, KeyboardFocusChangedEventArgs e) => T(sender, e);
		private void Window_PreviewLostKeyboardFocus(Object sender, KeyboardFocusChangedEventArgs e) => T(sender, e);
		#endregion

		#endregion

		#region 1.1.4.3 StylusEventArgs
		private void Window_GotStylusCapture(Object sender, StylusEventArgs e) => T(sender, e);
		private void Window_LostStylusCapture(Object sender, StylusEventArgs e) => T(sender, e);
		private void Window_PreviewStylusInAirMove(Object sender, StylusEventArgs e) => T(sender, e);
		private void Window_PreviewStylusInRange(Object sender, StylusEventArgs e) => T(sender, e);
		private void Window_PreviewStylusMove(Object sender, StylusEventArgs e) => T(sender, e);
		private void Window_PreviewStylusOutOfRange(Object sender, StylusEventArgs e) => T(sender, e);
		private void Window_PreviewStylusUp(Object sender, StylusEventArgs e) => T(sender, e);
		private void Window_StylusEnter(Object sender, StylusEventArgs e) => T(sender, e);
		private void Window_StylusInAirMove(Object sender, StylusEventArgs e) => T(sender, e);
		private void Window_StylusInRange(Object sender, StylusEventArgs e) => T(sender, e);
		private void Window_StylusLeave(Object sender, StylusEventArgs e) => T(sender, e);
		private void Window_StylusMove(Object sender, StylusEventArgs e) => T(sender, e);
		private void Window_StylusOutOfRange(Object sender, StylusEventArgs e) => T(sender, e);
		private void Window_StylusUp(Object sender, StylusEventArgs e) => T(sender, e);

		#region 1.1.4.3.1 StylusButtonEventArgs
		private void Window_PreviewStylusButtonDown(Object sender, StylusButtonEventArgs e) => T(sender, e);
		private void Window_PreviewStylusButtonUp(Object sender, StylusButtonEventArgs e) => T(sender, e);
		private void Window_StylusButtonDown(Object sender, StylusButtonEventArgs e) => T(sender, e);
		private void Window_StylusButtonUp(Object sender, StylusButtonEventArgs e) => T(sender, e);
		#endregion

		#region 1.1.4.3.2 StylusDownEventArgs
		private void Window_PreviewStylusDown(Object sender, StylusDownEventArgs e) => T(sender, e);
		private void Window_StylusDown(Object sender, StylusDownEventArgs e) => T(sender, e);
		#endregion

		#region 1.1.4.3.3 StylusSystemGestureEventArgs
		private void Window_PreviewStylusSystemGesture(Object sender, StylusSystemGestureEventArgs e) => T(sender, e);
		private void Window_StylusSystemGesture(Object sender, StylusSystemGestureEventArgs e) => T(sender, e);
		#endregion

		#endregion

		#region 1.1.4.4 TouchEventArgs
		private void Window_GotTouchCapture(Object sender, TouchEventArgs e) => T(sender, e);
		private void Window_LostTouchCapture(Object sender, TouchEventArgs e) => T(sender, e);
		private void Window_PreviewTouchDown(Object sender, TouchEventArgs e) => T(sender, e);
		private void Window_PreviewTouchMove(Object sender, TouchEventArgs e) => T(sender, e);
		private void Window_PreviewTouchUp(Object sender, TouchEventArgs e) => T(sender, e);
		private void Window_TouchDown(Object sender, TouchEventArgs e) => T(sender, e);
		private void Window_TouchEnter(Object sender, TouchEventArgs e) => T(sender, e);
		private void Window_TouchLeave(Object sender, TouchEventArgs e) => T(sender, e);
		private void Window_TouchMove(Object sender, TouchEventArgs e) => T(sender, e);
		private void Window_TouchUp(Object sender, TouchEventArgs e) => T(sender, e);
		#endregion

		#region 1.1.4.5 TextCompositionEventArgs
		private void Window_PreviewTextInput(Object sender, TextCompositionEventArgs e) => T(sender, e);
		private void Window_TextInput(Object sender, TextCompositionEventArgs e) => T(sender, e);
		#endregion

		#region 1.1.4.6 Other
		private void Window_ManipulationBoundaryFeedback(Object sender, ManipulationBoundaryFeedbackEventArgs e) => T(sender, e);
		private void Window_ManipulationCompleted(Object sender, ManipulationCompletedEventArgs e) => T(sender, e);
		private void Window_ManipulationDelta(Object sender, ManipulationDeltaEventArgs e) => T(sender, e);
		private void Window_ManipulationInertiaStarting(Object sender, ManipulationInertiaStartingEventArgs e) => T(sender, e);
		private void Window_ManipulationStarted(Object sender, ManipulationStartedEventArgs e) => T(sender, e);
		private void Window_ManipulationStarting(Object sender, ManipulationStartingEventArgs e) => T(sender, e);
		#endregion

		#endregion

		#region 1.1.5 QueryContinueDragEventArgs
		private void Window_PreviewQueryContinueDrag(Object sender, QueryContinueDragEventArgs e) => T(sender, e);
		private void Window_QueryContinueDrag(Object sender, QueryContinueDragEventArgs e) => T(sender, e);
		#endregion

		#region 1.1.6 DataTransferEventArgs
		private void Window_SourceUpdated(Object sender, DataTransferEventArgs e) => T(sender, e);
		private void Window_TargetUpdated(Object sender, DataTransferEventArgs e) => T(sender, e);
		#endregion

		#region 1.1.7 ToolTipEventArgs
		private void Window_ToolTipClosing(Object sender, ToolTipEventArgs e) => T(sender, e);
		private void Window_ToolTipOpening(Object sender, ToolTipEventArgs e) => T(sender, e);
		#endregion

		#region 1.1.8 Other
		private void Window_DpiChanged(Object sender, DpiChangedEventArgs e) => T(sender, e);

		private void Window_RequestBringIntoView(Object sender, RequestBringIntoViewEventArgs e) => T(sender, e);

		private void T(Object sender, SizeChangedEventArgs e, [CallerMemberName] String memberName = "") =>
			Trace.WriteLine(
				ShowBase(memberName) + 
				(e.HeightChanged ? $" Height: {e.PreviousSize.Height} => {e.NewSize.Height}" : String.Empty) +
				(e.WidthChanged ? $" Width: {e.PreviousSize.Width} => {e.NewSize.Width}" : String.Empty)
			);
		private void Window_SizeChanged(Object sender, SizeChangedEventArgs e) => T(sender, e);
		#endregion

		#endregion

		#region 1.2 CancelEventArgs
		private void Window_Closing(Object sender, CancelEventArgs e) => T(sender, e);
		#endregion

		#endregion

		#region 2. DependencyPropertyChangedEventArgs

		private void T(Object sender, DependencyPropertyChangedEventArgs e, [CallerMemberName] String memberName = "") => 
			Trace.WriteLine(ShowBase(memberName) + $" {e.Property.Name}: {e.OldValue} => {e.NewValue}");

		private void Window_DataContextChanged(Object sender, DependencyPropertyChangedEventArgs e) => T(sender, e);
		private void Window_FocusableChanged(Object sender, DependencyPropertyChangedEventArgs e) => T(sender, e);
		private void Window_IsEnabledChanged(Object sender, DependencyPropertyChangedEventArgs e) => T(sender, e);
		private void Window_IsHitTestVisibleChanged(Object sender, DependencyPropertyChangedEventArgs e) => T(sender, e);
		private void Window_IsKeyboardFocusWithinChanged(Object sender, DependencyPropertyChangedEventArgs e) => T(sender, e);
		private void Window_IsKeyboardFocusedChanged(Object sender, DependencyPropertyChangedEventArgs e) => T(sender, e);
		private void Window_IsMouseCaptureWithinChanged(Object sender, DependencyPropertyChangedEventArgs e) => T(sender, e);
		private void Window_IsMouseCapturedChanged(Object sender, DependencyPropertyChangedEventArgs e) => T(sender, e);
		private void Window_IsMouseDirectlyOverChanged(Object sender, DependencyPropertyChangedEventArgs e) => T(sender, e);
		private void Window_IsStylusCaptureWithinChanged(Object sender, DependencyPropertyChangedEventArgs e) => T(sender, e);
		private void Window_IsStylusCapturedChanged(Object sender, DependencyPropertyChangedEventArgs e) => T(sender, e);
		private void Window_IsStylusDirectlyOverChanged(Object sender, DependencyPropertyChangedEventArgs e) => T(sender, e);
		private void Window_IsVisibleChanged(Object sender, DependencyPropertyChangedEventArgs e) => T(sender, e);

		#endregion
	}
}
