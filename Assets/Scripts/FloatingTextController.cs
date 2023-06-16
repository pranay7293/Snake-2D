using System.Collections;
using UnityEngine;

public class FloatingTextController : MonoBehaviour
{
    private Animator _animator;
    private TextMesh _textMesh;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _textMesh = GetComponent<TextMesh>();
    }
    public void ShowText(string text)
    {
        _textMesh.text = text;
        _animator.SetTrigger("ShowTrigger");
    }
}