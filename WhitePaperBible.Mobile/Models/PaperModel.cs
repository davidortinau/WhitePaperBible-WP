using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using WhitePaperBible.Data;

namespace WhitePaperBible.Mobile.Models
{
    [DataContract]
    public class PaperModel
    {
        public PaperModel()
        {
        }

        private ObservableCollection<Paper> _papers; 

        [DataMember]
        public ObservableCollection<Paper> Papers { 
            get
            {
                if(_papers == null)
                    _papers = new ObservableCollection<Paper>();

                return _papers;
            } 
            set { _papers = value; }
        }
    
        public static PaperModel FromFile()
        {
            PaperModel pm = null;
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using(IsolatedStorageFileStream stream = new IsolatedStorageFileStream("paperModel.dat", FileMode.OpenOrCreate, isf))
                {
                    if(stream.Length > 0)
                    {
                        DataContractSerializer dcs = new DataContractSerializer(typeof(PaperModel));
                        pm = dcs.ReadObject(stream) as PaperModel;
                    }
                }
            }

            if(pm == null)
            {
                pm = new PaperModel();
            }

            return pm;
        }

        public void SaveToFile()
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("paperModel.dat", FileMode.Create, isf))
                {
                    DataContractSerializer dcs = new DataContractSerializer(typeof(PaperModel));
                    dcs.WriteObject(stream, this);
                    
                }
            }
        }
    }
}
