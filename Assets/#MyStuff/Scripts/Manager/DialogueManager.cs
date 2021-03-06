﻿using UnityEngine;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{
    #region Singleton
    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        if ( Instance != null && Instance != this )
        {
            Destroy( this );
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    [SerializeField] private DialogueRunner yarnRunner;
    public DialogueRunner YarnRunner
    {
        get => yarnRunner;
        private set => yarnRunner = value;
    }
    [SerializeField] private DialogueUI yarnUI;
    public DialogueUI YarnUI
    {
        get => yarnUI;
        private set => yarnUI = value;
    }

    public void CheckForOptions()
    {
        
    }
}
