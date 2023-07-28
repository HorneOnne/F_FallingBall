using UnityEngine;

namespace FallingBall
{
    public class FallingBallCanvas : MonoBehaviour
    {
        [SerializeField] protected Canvas _canvas;

        #region Properties
        public Canvas Canvas { get { return _canvas; } }
        #endregion

        public virtual void DisplayCanvas(bool isDisplay)
        {
            _canvas.enabled = isDisplay;
        }
    }
}
