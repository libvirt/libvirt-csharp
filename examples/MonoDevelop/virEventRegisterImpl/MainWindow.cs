/*
 * Copyright (C)
 *   Arnaud Champion <arnaud.champion@devatom.fr>
 *   Jaromír Červenka <cervajz@cervajz.com>
 *
 * See COPYING.LIB for the License of this software
 *
 * Sample code for :
 * Function :
 *      Connect.DomainEventRegister
 *      Connect.OpenAuth
 *      Event.RegisterImpl
 *
 *
 * Types :
 *      EventHandleType
 *      EventHandleCallback
 *      FreeCallback
 *      EventTimeoutCallback
 *      DomainEventType
 *      DomainEventDefinedDetailType
 *      DomainEventUndefinedDetailType
 *      DomainEventResumedDetailType
 *      DomainEventStartedDetailType
 *      DomainEventStoppedDetailType
 *      DomainEventSuspendedDetailType
 *
 */

using System;
using Gtk;
using Libvirt;

public partial class MainWindow : Gtk.Window
{
	private int _fd;
    private EventHandleType _event;
    private EventHandleCallback _cb;
    private FreeCallback _ff;
    private IntPtr _opaque;
    private int _active;
    private int _timeout;
    private EventTimeoutCallback _tcb;
    private IntPtr _conn;
	private TextBuffer _textBuf;

	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();
		GLib.Timeout.Add(50, new GLib.TimeoutHandler(TimerCallback));
		_textBuf = textview1.Buffer;
		Event.RegisterImpl(AddHandleFunc, UpdateHandleFunc, RemoveHandleFunc, AddTimeoutFunc, UpdateTimeoutFunc,
                               RemoveTimeoutFunc);
	}

	private bool TimerCallback()
    {
        if (_tcb != null && _active == 1)
            _tcb(_timeout, _opaque);

        if (_cb != null)
        {
            _cb(0,
                 _fd,
                 (int)_event,
                 _opaque);
        }

		return true;
    }

	private void FreeFunc(IntPtr opaque)
        {
        }

        private int RemoveTimeoutFunc(int timer)
        {
            _active = 0;
            if (_ff != null)
                _ff(_opaque);
            return 0;
        }

        private void UpdateTimeoutFunc(int timer, int timeout)
        {
            _timeout = timeout;
        }

        private int AddTimeoutFunc(int timeout, EventTimeoutCallback cb, IntPtr opaque, FreeCallback ff)
        {
            _active = 1;
            _timeout = timeout;
            _tcb = cb;
            _ff = ff;
            _opaque = opaque;
            return 0;
        }

        private int RemoveHandleFunc(int watch)
        {
            _fd = 0;
            if (_ff != null)
                _ff(_opaque);
            return 0;
        }
        private void UpdateHandleFunc(int watch, int events)
        {
            _event = (EventHandleType)events;
        }

        private int AddHandleFunc(int fd, int events, EventHandleCallback cb, IntPtr opaque, FreeCallback ff)
        {
            _fd = fd;
            _event = (EventHandleType)events;
            _cb = cb;
            _ff = ff;
            _opaque = opaque;
            return 0;
        }

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected virtual void OnButton1Clicked (object sender, System.EventArgs e)
	{
		_conn = Connect.Open(entry1.Text);
        int cbInstall = Connect.DomainEventRegister(_conn, DomainEventCallback, IntPtr.Zero, FreeFunc);
        if (cbInstall == 0)
        {
			_textBuf.Text = "Connection Done.\r\n";
        }
	}

	private void DomainEventCallback(IntPtr conn, IntPtr dom, DomainEventType evt, int detail, IntPtr opaque)
    {
        switch (evt)
        {
            case DomainEventType.VIR_DOMAIN_EVENT_DEFINED:
				_textBuf.Text += "Domain defined :";
                switch ((DomainEventDefinedDetailType)detail)
                {
                    case DomainEventDefinedDetailType.VIR_DOMAIN_EVENT_DEFINED_ADDED:
						_textBuf.Text += "domain added\r\n";
                        break;
                    case DomainEventDefinedDetailType.VIR_DOMAIN_EVENT_DEFINED_UPDATED:
						_textBuf.Text += "domain updated\r\n";
                        break;
                }
                break;
            case DomainEventType.VIR_DOMAIN_EVENT_UNDEFINED:
				_textBuf.Text += "Domain undefined :";
                switch ((DomainEventUndefinedDetailType)detail)
                {
                    case DomainEventUndefinedDetailType.VIR_DOMAIN_EVENT_UNDEFINED_REMOVED:
						_textBuf.Text += "domain removeed\r\n";
                        break;
                }
                break;
            case DomainEventType.VIR_DOMAIN_EVENT_RESUMED:
				_textBuf.Text += "Domain resumed :";
                switch ((DomainEventResumedDetailType)detail)
                {
                    case DomainEventResumedDetailType.VIR_DOMAIN_EVENT_RESUMED_MIGRATED:
						_textBuf.Text += "domain migrated\r\n";
                        break;
                    case DomainEventResumedDetailType.VIR_DOMAIN_EVENT_RESUMED_UNPAUSED:
						_textBuf.Text += "domain unpaused\r\n";
                        break;
                }
                break;
            case DomainEventType.VIR_DOMAIN_EVENT_STARTED:
				_textBuf.Text += "Domain started :";
                switch ((DomainEventStartedDetailType)detail)
                {
                    case DomainEventStartedDetailType.VIR_DOMAIN_EVENT_STARTED_BOOTED:
						_textBuf.Text += "domain booted\r\n";
                        break;
                    case DomainEventStartedDetailType.VIR_DOMAIN_EVENT_STARTED_MIGRATED:
						_textBuf.Text += "domain migrated\r\n";
                        break;
                    case DomainEventStartedDetailType.VIR_DOMAIN_EVENT_STARTED_RESTORED:
						_textBuf.Text += "domain restored\r\n";
                        break;
                }
                break;
            case DomainEventType.VIR_DOMAIN_EVENT_STOPPED:
				_textBuf.Text += "Domain stopped :";
                switch ((DomainEventStoppedDetailType)detail)
                {
                    case DomainEventStoppedDetailType.VIR_DOMAIN_EVENT_STOPPED_CRASHED:
						_textBuf.Text += "domain crashed\r\n";
                        break;
                    case DomainEventStoppedDetailType.VIR_DOMAIN_EVENT_STOPPED_DESTROYED:
						_textBuf.Text += "domain destroyed\r\n";
                        break;
                    case DomainEventStoppedDetailType.VIR_DOMAIN_EVENT_STOPPED_FAILED:
						_textBuf.Text += "domain failed\r\n";
                        break;
                    case DomainEventStoppedDetailType.VIR_DOMAIN_EVENT_STOPPED_MIGRATED:
						_textBuf.Text += "domain migrated\r\n";
                        break;
                    case DomainEventStoppedDetailType.VIR_DOMAIN_EVENT_STOPPED_SAVED:
						_textBuf.Text += "domain saved\r\n";
                        break;
                    case DomainEventStoppedDetailType.VIR_DOMAIN_EVENT_STOPPED_SHUTDOWN:
						_textBuf.Text += "domain shutdown\r\n";
                        break;
                }
                break;
            case DomainEventType.VIR_DOMAIN_EVENT_SUSPENDED:
				_textBuf.Text += "Domain suspended :";
                switch ((DomainEventSuspendedDetailType)detail)
                {
                    case DomainEventSuspendedDetailType.VIR_DOMAIN_EVENT_SUSPENDED_MIGRATED:
						_textBuf.Text += "domain migrated\r\n";
                        break;
                    case DomainEventSuspendedDetailType.VIR_DOMAIN_EVENT_SUSPENDED_PAUSED:
						_textBuf.Text += "domain paused\r\n";
                        break;
                }
                break;
        }
    }
}
