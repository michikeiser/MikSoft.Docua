namespace MikSoft.Docua.Common.Interfaces
{
    using DryIoc;

    public interface IDryIocRegistration
    {
        void Load(IRegistrator registrator);
    }
}