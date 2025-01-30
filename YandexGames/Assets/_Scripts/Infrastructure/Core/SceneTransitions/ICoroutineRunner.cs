using System.Collections;
using UnityEngine;

namespace _Scripts.Infrastructure.Core.SceneTransitions
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
    }
}