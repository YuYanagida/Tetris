using System.Collections.Generic;

namespace Game.Tetris.Common
{
    public interface ILoading
    {

    }

    public class OnLoadingData
    {
        private List<ILoading> _onLoadingClass = new();

        public bool IsCompleteLoad => _onLoadingClass.Count == 0;

        public void BeginLoading(ILoading loadClass)
        {
            _onLoadingClass.Add(loadClass);
        }

        public void CompleteLoad(ILoading loadingClass)
        {
            _onLoadingClass.Remove(loadingClass);
        }
    }
}