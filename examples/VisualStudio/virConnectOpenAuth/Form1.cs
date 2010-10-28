/*
 * Copyright (C)
 *   Arnaud Champion <arnaud.champion@devatom.fr>
 *   Jaromír Červenka <cervajz@cervajz.com>
 *
 * See COPYING.LIB for the License of this software
 * 
 * Sample code for :
 * Function :
 *      Connect.OpenAuth
 *      Connect.NumOfDefinedDomains
 *      Connect.ListDefinedDomains
 *      Connect.NumOfDomains
 *      Connect.Close
 *      Connect.ListDomains
 *      Domain.LookupByID
 *      Domain.GetName
 *
 * Types :
 *      ConnectAuth
 *      ConnectCredential
 *      ConnectCredentialType
 */

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Libvirt;

namespace virConnectOpenAuth
{
    struct AuthData
    {
        public string user_name;
        public string password;
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            lbDomains.Items.Clear();

            // Fill a structure to pass username and password to callbacks
            AuthData authData = new AuthData { password = tbPassword.Text, user_name = tbUsername.Text };
            IntPtr authDataPtr = Marshal.AllocHGlobal(Marshal.SizeOf(authData));
            Marshal.StructureToPtr(authData, authDataPtr, true);
            // Fill a virConnectAuth structure
            ConnectAuth auth = new ConnectAuth
            {
                cbdata = authDataPtr,               // The authData structure
                cb = AuthCallback,                  // the method called by callbacks
                CredTypes = new[]
                                {
                                    ConnectCredentialType.VIR_CRED_AUTHNAME,
                                    ConnectCredentialType.VIR_CRED_PASSPHRASE
                                }          // The list of credentials types
            };

            // Request the connection
            IntPtr conn = Connect.OpenAuth(tbURI.Text, ref auth, 0);
            Marshal.FreeHGlobal(authDataPtr);

            if (conn != IntPtr.Zero)
            {
                // Get the number of defined (not running) domains
                int numOfDefinedDomains = Connect.NumOfDefinedDomains(conn);
                string[] definedDomainNames = new string[numOfDefinedDomains];
                if (Connect.ListDefinedDomains(conn, ref definedDomainNames, numOfDefinedDomains) == -1)
                {
                    MessageBox.Show("Unable to list defined domains", "List defined domains failed", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    goto cleanup;
                }

                // Add the domain names to the listbox
                foreach (string domainName in definedDomainNames)
                    lbDomains.Items.Add(domainName);

                // Get the number of running domains
                int numOfRunningDomain = Connect.NumOfDomains(conn);
                int[] runningDomainIDs = new int[numOfRunningDomain];
                if (Connect.ListDomains(conn, runningDomainIDs, numOfRunningDomain) == -1)
                {
                    MessageBox.Show("Unable to list running domains", "List running domains failed", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    goto cleanup;
                }

                // Add the domain names to the listbox
                foreach (int runningDomainID in runningDomainIDs)
                {
                    IntPtr domainPtr = Domain.LookupByID(conn, runningDomainID);
                    if (domainPtr == IntPtr.Zero)
                    {
                        MessageBox.Show("Unable to lookup domains by id", "Lookup domain failed", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                        goto cleanup;
                    }
                    string domainName = Domain.GetName(domainPtr);
                    Domain.Free(domainPtr);
                    if (string.IsNullOrEmpty(domainName))
                    {
                        MessageBox.Show("Unable to get domain name", "Get domain name failed", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                        goto cleanup;
                    }
                    lbDomains.Items.Add(domainName);
                }

            cleanup:
                Connect.Close(conn);
            }
            else
            {
                MessageBox.Show("Unable to connect", "Connection failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
}
