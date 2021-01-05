using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour
{
    public bool IsFirebaseReady { get; private set; }
    public bool IsSignInOnProgress { get; private set; }

    public TMP_InputField emailField;
    public TMP_InputField passwordField;
    public Button signinButton;

    public static FirebaseApp firebaseApp;
    public static FirebaseAuth firebaseAuth;

    public static FirebaseUser User;

    //private string sn = "Lobby";
    
    // Start is called before the first frame update
    void Start()
    {
        //Screen.SetResolution(640, 480, true);

        signinButton.interactable = false;

        //if continuewith make error use ContinueWithOnMainThread
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                var result = task.Result;

                if (result != DependencyStatus.Available)
                {
                    Debug.LogError(result.ToString());
                    IsFirebaseReady = false;
                }
                else
                {
                    IsFirebaseReady = true;

                    firebaseApp = FirebaseApp.DefaultInstance;
                    firebaseAuth = FirebaseAuth.DefaultInstance;
                }

                signinButton.interactable = IsFirebaseReady;
            }
        );
    }

    public void Signin()
    {
        if (!IsFirebaseReady || IsSignInOnProgress || User != null)
        {
            return;
        }

        IsSignInOnProgress = true;
        signinButton.interactable = false;

        //if you are gonna use specifically google or apple account use
        //firebaseAuth.SignInWithCredentialAsync()
        firebaseAuth.SignInWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWithOnMainThread(task =>
            {
                Debug.Log($"Sign in Status: {task.Status}");

                IsSignInOnProgress = false;
                signinButton.interactable = true;

                if (task.IsFaulted)
                {
                    Debug.LogError(task.Exception);
                }
                else if (task.IsCanceled)
                {
                    Debug.LogError("Sign in canceled");
                }
                else
                {
                    User = task.Result;
                    Debug.Log(User.Email);
                    SceneManager.LoadScene("Lobby");
                }
            }
        );
    }
}
