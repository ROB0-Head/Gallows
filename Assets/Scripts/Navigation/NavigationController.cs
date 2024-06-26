using DG.Tweening;
using Settings;
using UI.Screens;
using UnityEngine;
using Utils;


namespace Navigation
{
    public class NavigationController : MonoSingleton<NavigationController>
    {
        [SerializeField] private GameObject _canvas;
        
        private DefaultScreen _currentScreen;

        public GameObject Canvas => _canvas;
        
        public DefaultScreen CurrentScreen => _currentScreen;

        void Start()
        {
            ScreenTransition<MainScreen>();
        }

        public void UpdateCanvas()
        {
            UnityEngine.Canvas.ForceUpdateCanvases();
        }

        public void ScreenTransition<T>(ScreenSettings settings = null) where T : DefaultScreen
        {
            if (_currentScreen != null)
            {
                var startPos = _currentScreen is MainScreen ? 1000 : -1000;
                var endPos = _currentScreen is MainScreen ? -1000 : 1000;

                Sequence mySequence = DOTween.Sequence();
                var screen = _currentScreen;

                _currentScreen = Instantiate(SettingsProvider.Get<PrefabSet>().GetScreen<T>(), _canvas.transform);
                _currentScreen.gameObject.transform.localPosition = new Vector3(startPos, 0, 0);
                screen.Deactivate();
                _currentScreen.Setup(settings);
                mySequence.AppendCallback(() => _currentScreen.gameObject.transform.DOLocalMoveX(0, 0.2f));
                mySequence.Append(screen.gameObject.transform.DOLocalMoveX(endPos, 0.2f));
                mySequence.AppendCallback(() => Destroy(screen.gameObject));
                mySequence.Play();
            }
            else
            {
                _currentScreen = Instantiate(SettingsProvider.Get<PrefabSet>().GetScreen<T>(), _canvas.transform);
                _currentScreen.Setup(settings);
            }
        }
        
        
        
    }
}