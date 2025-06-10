using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MandelbrotGenerator
{
	internal class MandelbrotSet(Int32 maxIterations, Double escapeRadius)
	{
		public Int32 MaxIterations => maxIterations;

		public Double EscapeRadius => escapeRadius;

		public Int32 GetEscapeIterations(Complex c)
		{
			var z = Complex.Zero;
			for (var i = 0; i < maxIterations; ++i) {
				if (AbsSquare(z) > escapeRadiusSquare) {
					return i;
				}
				z = Square(z) + c;
			}
			return maxIterations;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Double Square(Double value) => value * value;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Complex Square(Complex value) => value * value;

		private static Double AbsSquare(Complex c) => Square(c.Real) + Square(c.Imaginary);

		private readonly Double escapeRadius = escapeRadius;
		private readonly Double escapeRadiusSquare = escapeRadius * escapeRadius;
		private readonly Int32 maxIterations = maxIterations;
	}

	class MandelbrotImage
	{
		public static Byte[] Create(	Int32 pixelWidth,
										Int32 pixelHight,
										Complex center,
										Double zoomFactor,
										Int32 maxIterations,
										Double escapeRadius,
										Double aspectRatio = 1.0)
		{
			var realWidth = pixelWidth / zoomFactor;
			var realMin   = center.Real - realWidth / 2;
			var realStep  = realWidth / pixelWidth;

			var imaginaryHight = pixelHight / zoomFactor * aspectRatio;
			var imaginaryMin   = center.Imaginary - imaginaryHight / 2;
			var imaginaryStep  = imaginaryHight / pixelHight;

			var result = new Byte[pixelWidth * pixelHight];

			var mandelbrotSet = new MandelbrotSet(maxIterations, escapeRadius);

			var index = 0;
			var real = realMin;
			for (var x = 0; x < pixelWidth; ++x) {
				var imaginary = imaginaryMin;
				for (var y = 0; y < pixelHight; ++y) {
					result[index++] = (Byte) (mandelbrotSet.GetEscapeIterations(new Complex(real, imaginary)) * Byte.MaxValue / maxIterations);
					imaginary += imaginaryStep;
				}
				real += realStep;
			}

			return result;
		}

	}
}
