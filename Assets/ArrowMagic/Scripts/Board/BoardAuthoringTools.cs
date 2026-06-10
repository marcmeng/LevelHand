using UnityEngine;

namespace PixelBug.ArrowMagic
{
    [DisallowMultipleComponent]
    public sealed class BoardAuthoringTools : MonoBehaviour
    {
        [InspectorName("控制器")]
        [SerializeField] BoardController controller;

        public BoardController Controller
        {
            get
            {
                if (controller == null)
                    controller = GetComponent<BoardController>();

                return controller;
            }
        }

        void Reset()
        {
            if (controller == null)
                controller = GetComponent<BoardController>();
        }

        public void AutoAssign()
        {
            if (controller == null)
                controller = GetComponent<BoardController>();
        }
    }
}
