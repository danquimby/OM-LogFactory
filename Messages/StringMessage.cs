using System;

namespace OM_Logger.Messages
{
	/// <summary>
	/// </summary>
	public class StringMessage: AbstractMessage
	{
    private string _Message;

    public string Message
    {
      get
      {
        return _Message;
      }
      set
      {
        _Message = value;
      }
    }
	}
}
