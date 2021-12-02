namespace PrensaAPI.Modelos
{
    public interface IObservable
    {
        public void addObserver(string url);
        public void removeObserver(string url);

        public string llevarBultoALaPila(Bulto bulto);
    }
}