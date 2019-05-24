﻿using System;

public class TimerPlus : System.Timers.Timer
{
    private DateTime m_dueTime;

    public TimerPlus() : base()
    {
        this.Elapsed += this.ElapsedAction;
    }

    protected new void Dispose()
    {
        this.Elapsed -= this.ElapsedAction;
        base.Dispose();
    }

    public double TimeLeft => (this.m_dueTime - DateTime.Now).TotalMilliseconds;

    public void AddInterval(double time)
    {
        if (this.Enabled)
        {
            Stop();
            this.Interval = this.TimeLeft + time;
            Start();
        }
        else
        {
            this.Interval = this.TimeLeft + time;
        }
    }

    public new void Start()
    {
        this.m_dueTime = DateTime.Now.AddMilliseconds(this.Interval);
        base.Start();
    }

    private void ElapsedAction(object sender, System.Timers.ElapsedEventArgs e)
    {
        if (this.AutoReset)
        {
            this.m_dueTime = DateTime.Now.AddMilliseconds(this.Interval);
        }
    }
}