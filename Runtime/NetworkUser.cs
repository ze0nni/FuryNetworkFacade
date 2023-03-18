using UnityEngine;

namespace Fury
{
    public class NetworkUser
    {
        readonly NetworkController _controller;
        public readonly string Id;

        public string Nickname { get; private set; }
        public Sprite Photo { get; private set; }

        public NetworkUser(NetworkController controller, string id)
        {
            _controller = controller;
            Id = id;
        }

        internal void UpdateNickname(string nickname)
        {
            Nickname = nickname;
            _controller.SetDirty();
        }

        internal void UpdatePhoto(Sprite photo)
        {
            Photo = photo;
            _controller.SetDirty();
        }

        internal void UpdatePhoto(string photoSource)
        {
            //
            _controller.SetDirty();
        }
    }
}