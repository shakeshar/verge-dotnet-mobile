using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Verge.Core.Client;
using Verge.Mobile.Models;
using Verge.Mobile.Services;
using Xamarin.Forms;

namespace Verge.Mobile.ViewModels
{

    public class NodeStatusViewModel : CollectionViewModel<NodeStatusItemViewModel>
    {
       
        
        
        #region Properties
        public ICommand SaveCmd { get; private set; }
      
        public ICommand SelectedItemCmd { get; private set; }
        List<RPCCredentials> test;
        #endregion
        
        public NodeStatusViewModel()
        {
            SelectedItemCmd = new Command(async () => await Edit(), () => CanStart);
            SaveCmd = new Command(async () => await Save(), () => CanStart);
          
        }

        private async Task Edit()
        {
            CanStart = false;
            IsBusy = true;
            ((App)App.Current).Cred = SelectedItem.Cred;
           await NavigationService.NavigateToAsync<EditNodeViewModel>();
            CanStart = true;
            IsBusy = false;
        }

        public override async Task OnApperaing()
        {
            test = Storage.GetItem<List<RPCCredentials>>(ConstantStrings.NODE_STATUS_KEY);
            Items.Clear();
            foreach (var item in test)
            {
                var client = Factories.VergeClientFactory.Create(item.Username, item.Password, item.Url, item.Port);
                var asd = new NodeStatusItemViewModel(client, item);
                Items.Add(asd);
                asd.Load();
               
            }
            IsBusy = true;
            base.OnApperaing();
            //await model.Load();
            //base.OnPropertyChanged(nameof(Balance));
            IsBusy = false;
        }
        private async Task Save()
        {
            CanStart = false;
            IsBusy = true;
            ((Command)SaveCmd).ChangeCanExecute();
            await NavigationService.NavigateToAsync<EditNodeViewModel>();
            try
            {
                
            }
            catch (Exception e)
            {
              await  NavigationService.Display(e.Message);
            }
            CanStart = true;
            IsBusy = false;
            ((Command)SaveCmd).ChangeCanExecute();
        }
    }
    public class NodeStatusItemViewModel : BaseViewModel
    {
        #region Fields
        private string name;
        private string url;
        private string ip;
        private string lastSeen;
        private string blocks;
        #endregion

        #region Properties
        public string Name { get { return name; } set { name = value; OnPropertyChanged(); } } 
        public string Url { get { return url; } set { url = value; OnPropertyChanged(); } }
        public string IP { get { return ip; } set { ip = value; OnPropertyChanged(); } }
        public string LastSeen { get { return lastSeen; } set { lastSeen = value; OnPropertyChanged(); } }
        public string Blocks { get { return blocks; } set { blocks = value; OnPropertyChanged(); } }
        public string Id { get; set; }
        #endregion
        private readonly IVergeClient client;
        public RPCCredentials Cred { get; }
        public NodeStatusItemViewModel(IVergeClient client, RPCCredentials cred)
        {
            this.Cred = cred;
            this.client = client;
            this.Url = Cred.Url;
            
            
        }
        public async Task Load()
        {
            while(true)
            {
               var result = await this.client.GetInfo();
                LastSeen = DateTimeOffset.Now.ToString();
                
                IP = result.Data.Result.ip;
                Blocks = result.Data.Result.blocks.ToString();
                await Task.Delay(1000);
                
            }
        }

    }

}
