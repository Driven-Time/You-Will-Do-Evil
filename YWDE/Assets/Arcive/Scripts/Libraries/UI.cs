using System.Collections;

using System.Collections.Generic;

using UnityEngine.UI;

using UnityEngine;


namespace UI
{
    /// <summary>
    /// Includes Methods for AutoScaling + Positioneing of UI elements at runtime
    /// </summary>
    public class UIElement
    {
        #region Variables
        /// <summary>
        /// Width of the Screen
        /// </summary>
        public float ScreenWidth;
        /// <summary>
        /// Height of the Screen
        /// </summary>
        public float ScreenHeight;

        /// <summary>
        /// Position of the Element on the X-Axis
        /// </summary>
        public float PositionX;

        /// <summary>
        /// Position of the Element on the Y-Axis
        /// </summary>
        public float PositionY;

        private float width;
        /// <summary>
        /// The Calculated Width of the element
        /// </summary>
        public float Width
        {
            get { return width; }
        }
        private float height;
        /// <summary>
        /// The Calculated Height of the element
        /// </summary>
        public float Height
        {
            get { return height; }
        }

        /// <summary>
        /// The Width of the element
        /// </summary>
        public float ElementWidth;
        /// <summary>
        /// The Height of the element
        /// </summary>
        public float ElementHeight;

        /// <summary>
        /// Offset of the Element on the X-Axis
        /// </summary>
        public float OffSetX = 0;
        /// <summary>
        /// Offset of the Element on the Y-Axis
        /// </summary>
        public float OffSetY = 0;

        private float Dimension;

        /// <summary>
        /// The UI Element
        /// </summary>
        public RectTransform Element;
        #endregion

        /// <summary>
        /// Auto adjusts the UI Element to fit the Aspect Ratio
        /// </summary>
        public void AutoScale ()
        {
            Debug.Log (Element.name + "Text");

            Positioning ();

            width = (ScreenWidth / ((ElementWidth == 0) ? (ElementWidth = 1) : (ElementWidth)));
            height = (ScreenHeight / ((ElementHeight == 0) ? (ElementHeight = 1): (ElementHeight)));
            
            Element.sizeDelta = new Vector2 (Width, Height);

            TextAlignement ();

        }

        /// <summary>
        /// Positions the UI Element
        /// </summary>
        private void Positioning ()
        {
            float _PositionX = ((PositionX == 0) ? (_PositionX = 0) : ((ScreenWidth / 2) * PositionX));
            float _PositionY = ((PositionY == 0) ? (_PositionY = 0) : ((ScreenHeight / 2) / PositionY));

            Element.localPosition = new Vector2 (_PositionX - OffSetX, _PositionY - OffSetY);
        }

        /// <summary>
        /// Aligns UI Text
        /// </summary>
        private void TextAlignement ()
        {
            Transform Text = Element.Find (Element.name + "Text");
            Text.GetComponent<RectTransform> ().sizeDelta = new Vector2 (ScreenWidth, Height * 2);
            Text.GetComponent<Text> ().fontSize = (int)Height;
        }
    }

}






