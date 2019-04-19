using Caliburn.Micro;
using System;
using System.ComponentModel.Composition;
///<see cref="MedHelper.ShellViewModel.SayHello(string)"/>
using System.Windows; // <- this should not be necessary


namespace MedHelper
{
    [Export(typeof(IShell))]
    public class ShellViewModel : PropertyChangedBase, IShell
    {
        public ShellViewModel()
        {
            Items = new BindableCollection<Model>
            {
                new Model { Id = Guid.NewGuid() },
                new Model { Id = Guid.NewGuid() },
                new Model { Id = Guid.NewGuid() },
                new Model { Id = Guid.NewGuid() }
            };
        }

        public BindableCollection<Model> Items { get; private set; }

        /// <summary>
        /// Adds a new Guid Object to the list
        /// </summary>
        public void Add()
        {
            Items.Add(new Model { Id = Guid.NewGuid() });
        }

        /// <summary>
        /// Caliburn Guardfunction for SayHello Button
        /// </summary>
        /// <param name="name">Object containing username variable from wpf</param>
        /// <returns></returns>
        public bool CanSayHello(string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }

        /// <summary>
        /// Removes a guid object from the list
        /// </summary>
        /// <param name="child"></param>
        public void Remove(Model child)
        {
            Items.Remove(child);
        }

        /// <summary>
        /// Dummy function to return the bound wpf object (username)
        /// </summary>
        /// <param name="name">Object containing username variable from wpf</param>
        /// <todo>Find better demo to pass back into wpf</todo>
        public void SayHello(string name)
        {
            MessageBox.Show(string.Format("Hello {0}!", name)); //Don't do this in real life :)
        }
    }
}