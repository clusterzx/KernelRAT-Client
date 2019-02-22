using Kernel.Rooting;
using Kernel.Nexus;
namespace Kernel {
    class Core {
        static void Main(string[] args) {
            Implementation.Initialization();
            Binder.RequestHandler.Request();
        }
    }
}
