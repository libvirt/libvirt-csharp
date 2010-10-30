/*
 * Copyright (C)
 *   Arnaud Champion <arnaud.champion@devatom.fr>
 *   Jaromír Červenka <cervajz@cervajz.com>
 *
 * See COPYING.LIB for the License of this software
 *
 * Sample code for :
 * Functions :
 *      Connect.OpenAuth
 *      Domain.GetInfo
 *      Domain.LookupByName
 *      Errors.GetLastError
 *      Errors.SetErrorFunc
 *
 * Types :
 *      DomainInfo
 *      Error
 *
 */

using System;
using Gtk;
using Libvirt;

public partial class MainWindow : Gtk.Window
{
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	protected virtual void OnButton1Clicked (object sender, System.EventArgs e)
	{
		// Connect to the host
		IntPtr conn = Connect.Open(entry1.Text);
		if (conn != IntPtr.Zero)
		{
			// Set error callback. The method "ErrorCallback" will be called when error raised
			Errors.SetErrorFunc(IntPtr.Zero, ErrorCallback);
			// Try to look up the domain by name
			IntPtr domain = Domain.LookupByName(conn, entry2.Text);
            DomainInfo di = new DomainInfo();
            Domain.GetInfo(domain, di);
			entry3.Text = di.State.ToString();
			entry4.Text = di.maxMem.ToString();
			entry5.Text = di.memory.ToString();
			entry6.Text = di.nrVirtCpu.ToString();
			entry7.Text = di.cpuTime.ToString();
		}
		else
		{
			// Get the last error
			Error err = Errors.GetLastError();
			ShowError(err);
		}
	}
	
	private void ErrorCallback(IntPtr userData, Error error)
    {
        ShowError(error);
    }
	
	private void ShowError(Error error)
	{
		MessageDialog errorMsg = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, error.Message);
		errorMsg.Title = "Error";
		if ((ResponseType) errorMsg.Run() == ResponseType.Close)
		{
			errorMsg.Destroy();
		}
	}
}

