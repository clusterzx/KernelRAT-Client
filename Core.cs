using Kernel.Rooting;
using Kernel.Nexus;
namespace Kernel {
    class Core {
        static void Main(string[] args) {
            //- Nice -//
            Implementation.Initialization();
            Binder.RequestHandler.Request();
        }
    }
}
