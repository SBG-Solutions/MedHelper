namespace MedHelper
{
    using Caliburn.Micro;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.Linq;
    using System.Windows;

    /// <summary>
    /// The Application BootStrap class.
    /// We are using a custom composite object-container to contain all information about the form.
    /// </summary>
    public class AppBootstrapper : BootstrapperBase
    {
        /// <summary>
        /// This is the actual container for the form-objects (liek the username)
        /// </summary>
        private CompositionContainer container;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        protected override void BuildUp(object instance) => container.SatisfyImportsOnce(instance);

        protected override void Configure()
        {
            var cat = new AggregateCatalog(
                AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()
                );

            container = new CompositionContainer(cat);

            var batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(container);

            container.Compose(batch);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = container.GetExportedValues<object>(contract);

            if (exports.Any())
                return exports.First();

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<IShell>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>We have to call this, so that the WPF resource-dic can bootstrap the depedency injection.</remarks>
        public AppBootstrapper() => Initialize();
    }
}