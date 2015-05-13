
using Android.Content;
using Android.Widget;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Util;
using Java.Lang;

namespace AncestorCloud.Droid
{
	public class RoundedImageView : ImageView
	{
		public RoundedImageView(Context ctx,IAttributeSet attr) : base(ctx,attr){
	
		}

		protected override void OnDraw (Android.Graphics.Canvas canvas)
		{
			base.OnDraw (canvas);

			Drawable drawable = Drawable;

			if (drawable == null) {
				return;
			}

			if (Width == 0 || Height == 0) {
				return;
			}
			Bitmap b = ((BitmapDrawable) drawable).Bitmap;
			Bitmap bitmap = b.Copy(Bitmap.Config.Argb8888, true);

			int w = Width, h = Height;

			Bitmap roundBitmap = GetRoundedCroppedBitmap(bitmap, w);
			canvas.DrawBitmap(roundBitmap, 0, 0, null);

	
			/*Paint paint = new Paint(PaintFlags.AntiAlias);
			Shader shader = new BitmapShader(bitmap, Shader.TileMode.Clamp, Shader.TileMode.Clamp);
			paint.SetShader(shader);

			// Draw a circle with the required radius.
			float halfWidth = canvas.Width/2;
			float halfHeight = canvas.Height/2;
			float radius = Math.Max(halfWidth, halfHeight);
			canvas.DrawCircle(halfWidth, halfHeight, radius, paint);
			*/
		}

		public static Bitmap GetRoundedCroppedBitmap(Bitmap bitmap, int radius) {
			Bitmap finalBitmap;
			if (bitmap.Width != radius || bitmap.Height != radius)
				finalBitmap = Bitmap.CreateScaledBitmap(bitmap, radius, radius,
					false);
			else
				finalBitmap = bitmap;
			Bitmap output = Bitmap.CreateBitmap(finalBitmap.Width,
				finalBitmap.Height, Bitmap.Config.Argb8888);
			Canvas canvas = new Canvas(output);

			Paint paint = new Paint();
			Rect rect = new Rect(0, 0, finalBitmap.Width,
				finalBitmap.Height);

			paint.AntiAlias = true;
			paint.FilterBitmap = true;
			paint.Dither = true;
			canvas.DrawARGB(0, 0, 0, 0);
			paint.Color = Color.ParseColor("#000000");
			canvas.DrawCircle(finalBitmap.Width / 2 + 0.7f,
				finalBitmap.Height / 2 + 0.7f,
				finalBitmap.Width / 2 + 0.1f, paint);
			
			paint.SetXfermode (new PorterDuffXfermode(PorterDuff.Mode.SrcIn)); 
			canvas.DrawBitmap(finalBitmap, rect, rect, paint);

			return output;
		}

	}
}



