/*
 * Copyright (C)
 *   Arnaud Champion <arnaud.champion@devatom.fr>
 *   Jaromír Červenka <cervajz@cervajz.com>
 *
 * See COPYING.LIB for the License of this software
 * 
 * Sample code for :
 * Function :
 *      virConnectOpen
 *      virConnectNumOfStoragePools
 *      virConnectListStoragePools
 */

using System;
using Gtk;
using Libvirt;
using System.Runtime.InteropServices;

public partial class MainWindow : Gtk.Window
{
	struct AuthData
    {
        public string user_name;
        public string password;
    }
	
	ListStore domainListStore = new ListStore(typeof(string));
	TreeViewColumn tvcDomains = new TreeViewColumn();
	
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();
		
		tvcDomains.Title = "Domains";
		treeview1.AppendColumn(tvcDomains);
		
		treeview1.Model = domainListStore;

		CellRendererText crtDomainCell = new CellRendererText();
		tvcDomains.PackStart(crtDomainCell, true);
		tvcDomains.AddAttribute(crtDomainCell, "text", 0);
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	protected virtual void OnButton1Clicked (object sender, System.EventArgs e)
	{
		// Fill a structure to pass username and password to callbacks
        AuthData authData = new AuthData { password = entry3.Text, user_name = entry2.Text };
        IntPtr authDataPtr = Marshal.AllocHGlobal(Marshal.SizeOf(authData));
        Marshal.StructureToPtr(authData, authDataPtr, false);
		
		// Fill a virConnectAuth structure
        ConnectAuth auth = new ConnectAuth
        {
            cbdata = authDataPtr,                  // The authData structure
            cb = AuthCallback,                  // the method called by callbacks
            CredTypes = new[]
                            {
                                ConnectCredentialType.VIR_CRED_AUTHNAME,
                                ConnectCredentialType.VIR_CRED_PASSPHRASE
                            }          // The list of credentials types
        };
		
		// Request the connection
        IntPtr conn = Connect.OpenAuth(entry1.Text, ref auth, 0);
		
		if (conn != IntPtr.Zero)
        {
            // Get the number of defined (not running) domains
            int numOfDefinedDomains = Connect.NumOfDefinedDomains(conn);
            string[] definedDomainNames = new string[numOfDefinedDomains];
            if (Connect.ListDefinedDomains(conn, ref definedDomainNames, numOfDefinedDomains) == -1)
            {
				ShowError("Unable to list defined domains");
                return;
            }
			
            // Add the domain names to the listbox
            foreach (string domainName in definedDomainNames)
			{
                AddDomainInTreeView(domainName);
			}
			
            // Get the number of running domains
            int numOfRunningDomain = Connect.NumOfDomains(conn);
            int[] runningDomainIDs = new int[numOfRunningDomain];
            if (Connect.ListDomains(conn, runningDomainIDs, numOfRunningDomain) == -1)
            {
                ShowError("Unable to list running domains");
                return;
            }
			
            // Add the domain names to the listbox
            foreach (int runningDomainID in runningDomainIDs)
            {
                IntPtr domainPtr = Domain.LookupByID(conn, runningDomainID);
                if (domainPtr == IntPtr.Zero)
                {
                    ShowError("Unable to lookup domains by id");
                    return;
                }
                string domainName = Domain.GetName(domainPtr);
                if (string.IsNullOrEmpty(domainName))
                {
                    ShowError("Unable to get domain name");
                    return;
                }
                AddDomainInTreeView(domainName);
            }
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
	
	private void AddDomainInTreeView(string domainName)
	{
		domainListStore.AppendValues(domainName);
	}
	
    private static int AuthCallback(ref ConnectCredential[] creds, IntPtr cbdata)
    {
        AuthData authData = (AuthData)Marshal.PtrToStructure(cbdata, typeof(AuthData));
        for (int i = 0; i < creds.Length; i++)
        {
            ConnectCredential cred = creds[i];
            switch (cred.type)
            {
                case ConnectCredentialType.VIR_CRED_AUTHNAME:
                    // Fill the user name
                    cred.Result = authData.user_name;
                    break;
                case ConnectCredentialType.VIR_CRED_PASSPHRASE:
                    // Fill the password
                    cred.Result = authData.password;
                    break;
                default:
                    return -1;
            }
        }
        return 0;
    }
	
	
}

