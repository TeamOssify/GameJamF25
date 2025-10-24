using System;

using UnityEngine;

public sealed class DialogStateMachine {
    private const int STOP_INDEX = int.MinValue;

    private readonly DialogFile.DialogEntry[] _dialogTree;

    private string _currentPlayerAnswer;
    private int _currentIndex = -1;

    public DialogStateMachine(DialogFile.DialogTree dialogTree) {
        _dialogTree = dialogTree.Tree;
    }

    public bool TryAdvance(out DialogFile.DialogEntry entry) {
        if (_currentIndex == STOP_INDEX || (_currentIndex >= 0 && _dialogTree[_currentIndex].Stop)) {
            entry = null;
            return false;
        }

        if (_currentPlayerAnswer == null) {
            _currentIndex++;
        }
        else {
            Debug.Assert(_dialogTree[_currentIndex].PlayerResponses != null, "_currentPlayerAnswer != null && _dialogTree[_currentEntry].PlayerResponses != null");

            var success = _dialogTree[_currentIndex].PlayerResponses.TryGetValue(_currentPlayerAnswer, out var label);
            Debug.Assert(success, "_dialogTree[_currentEntry].PlayerResponses.TryGetValue");

            _currentPlayerAnswer = null;

            for (var i = 0; i < _dialogTree.Length; i++) {
                if (_dialogTree[i].Label == label) {
                    _currentIndex = i;
                }
            }

            Debug.Assert(_dialogTree[_currentIndex].Label == label, "_dialogTree[_currentEntry].Label == label");
        }

        if (_currentIndex >= _dialogTree.Length) {
            entry = null;
            return false;
        }

        entry = _dialogTree[_currentIndex];
        return true;
    }

    public void UsePlayerAnswer(string message) {
        if (_currentIndex < 0) {
            return;
        }

        Debug.Assert(_currentPlayerAnswer == null, "_currentPlayerAnswer == null");
        _currentPlayerAnswer = message;
    }

    public void Exit() {
        _currentIndex = STOP_INDEX;
    }
}