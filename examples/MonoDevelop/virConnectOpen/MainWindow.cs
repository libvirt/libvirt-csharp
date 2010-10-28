using System;
using Gtk;
using Libvirt;

public partial class MainWindow : Gtk.Window
{
	ListStore StoragePoolListStore = new ListStore(typeof(string));
	TreeViewColumn tvcStoragePools = new TreeViewColumn();
	
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();
		
		tvcStoragePools.Title = "Storage pools";
		treeview1.AppendColumn(tvcStoragePools);
		
		treeview1.Model = StoragePoolListStore;
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	
	protected virtual void OnButton1Clicked (object sender, System.EventArgs e)
	{
        StoragePoolListStore.Clear ();

		IntPtr conn = Connect.Open(entry1.Text);
		
		if (conn != IntPtr.Zero)
        {
            int numOfStoragePools = Connect.NumOfStoragePools(conn);
            if (numOfStoragePools == -1)
            {
                ShowError("Unable to get the number of storage pools");
                goto cleanup;
            }
            string[] storagePoolsNames = new string[numOfStoragePools];
            int listStoragePools = Connect.ListStoragePools(conn, ref storagePoolsNames, numOfStoragePools);
            if (listStoragePools == -1)
            {
                ShowError("Unable to list storage pools");
                goto cleanup;
            }
            foreach (string storagePoolName in storagePoolsNames)
                AddStoragePoolInTreeView(storagePoolName);
        cleanup:
            Connect.Close(conn);
        }
        else
        {
            ShowError("Unable to connect");
        }
	}
	
	private void ShowError(string errorMessage)
	{
		MessageDialog errorMsg = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, errorMessage);
        errorMsg.Title = "Error";
		if ((ResponseType) errorMsg.Run() == ResponseType.Close)
		{ 
        	errorMsg.Destroy(); 
		}
	}
	
	private void AddStoragePoolInTreeView(string storagePoolName)
	{
		StoragePoolListStore.AppendValues(storagePoolName);
		CellRendererText crtStoragePoolCell = new CellRendererText();
		tvcStoragePools.PackStart(crtStoragePoolCell, true);
		tvcStoragePools.AddAttribute(crtStoragePoolCell, "text", 0);
	}
}

