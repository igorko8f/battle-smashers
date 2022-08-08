using CodeBase.Input;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        private InputService _inputService;
        
        public override void InstallBindings()
        {
            BindInputService();
        }
        
        private void BindInputService()
        {
#if UNITY_EDITOR
            _inputService = new StandaloneInput();
#else
            _inputService = new MobileInput();
#endif
            Container
                .Bind<IInputService>()
                .To<InputService>()
                .FromInstance(_inputService)
                .AsSingle();

        }
    }
}


