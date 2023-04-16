using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class FlexibleGridLayout : LayoutGroup
	{
		public enum FitType
		{
			Uniform,
			Width,
			Height,
			FixedRows,
			FixedColumns
		}

		public FitType fitType;
		public int rows;
		public int columns;
		public Vector2 cellSize;
		public Vector2 spacing;
		public bool fitX;
		public bool fitY;
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			
			/*
			// Scale the field parameters
			var scale = GetComponent<ResizeByAspect>().scaleFromRef;
			spacing = spacing * scale;
			padding.top = Mathf.CeilToInt(padding.top * scale);
			padding.bottom = Mathf.CeilToInt(padding.bottom * scale);
			padding.left = Mathf.CeilToInt(padding.left * scale);
			padding.right = Mathf.CeilToInt(padding.right * scale);
			*/

			if (fitType is FitType.Width or FitType.Height or FitType.Uniform)
			{
				fitX = true;
				fitY = true;
			
				var sqrRt = Mathf.Sqrt(transform.childCount);
				rows = Mathf.CeilToInt(sqrRt);
				columns = rows;
			}
		
			switch (fitType)
			{
				case FitType.FixedColumns:
				case FitType.Width:
					rows = Mathf.CeilToInt(transform.childCount / (float)columns);
					break;
				case FitType.FixedRows:
				case FitType.Height:
					columns = Mathf.CeilToInt(transform.childCount / (float)rows);
					break;
				case FitType.Uniform:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			var parentRect = rectTransform.rect;

			var cellWidth = (parentRect.width / columns) - ((spacing.x / columns) * (columns - 1)) - (padding.left / (float)columns) - (padding.right / (float)columns);
			var cellHeight = (parentRect.height / rows) - ((spacing.y / rows) * (rows - 1)) - (padding.top / (float)rows) - (padding.bottom / (float)rows);

			cellSize.x = fitX ? cellWidth : cellSize.x;
			cellSize.y = fitY ? cellHeight : cellSize.y;

			for (var i = 0; i < rectChildren.Count; i++)
			{
				var rowCount = i / rows;
				var columnCount = i % columns;

				var item = rectChildren[i];

				var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
				var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;
			
				SetChildAlongAxis(item, 0, xPos, cellSize.x);
				SetChildAlongAxis(item, 1, yPos, cellSize.y);
			}
		}
	
		public override void CalculateLayoutInputVertical()
		{
		}

		public override void SetLayoutHorizontal()
		{
		}

		public override void SetLayoutVertical()
		{
		}
	}
}