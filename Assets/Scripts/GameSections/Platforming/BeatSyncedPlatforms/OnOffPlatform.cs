using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BeatListener))]
public class OnOffPlatform : MonoBehaviour
{
    Color _onColor;
    [SerializeField] Color offColor = Color.grey;
    Collider2D _coll;
    SpriteRenderer _spriteRenderer;
    [SerializeField] bool inverted;
    bool _toggled = false;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _coll = GetComponent<Collider2D>();

        _onColor = _spriteRenderer.color;

        if (inverted) TogglePlatform();
    }

    public void TogglePlatform()
    {
        //Toggle off
        if(_toggled)
        {
            DisablePlatform();
            _toggled = false;
            return;
        }

        //Toggle on
        if (!_toggled)
        {
            EnablePlatform();
            _toggled = true;
            return;
        }
    }

    void EnablePlatform()
    {
        if (_coll != null) _coll.enabled = true;
        if (_spriteRenderer != null) _spriteRenderer.color = _onColor;
    }

    void DisablePlatform()
    {
        if (_coll != null) _coll.enabled = false;
        if (_spriteRenderer != null) _spriteRenderer.color = offColor;
    }
}
