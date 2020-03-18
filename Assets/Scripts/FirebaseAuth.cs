using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Firebase.Auth;

public class User 
{
    public string Name;
    public string Email;

    public User() { }

    public User(string name, string email)
    {
        this.Name = name;
        this.Email = email;
    }
}

public class FirebaseAuth : MonoBehaviour
{
    #region declaration

    public Firebase.FirebaseApp app;
    public Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    public GameObject gameUI;
    public GameObject connection;

    public pauseMenu pause;

    [SerializeField] private TMPro.TMP_InputField emailBox;
    [SerializeField] private TMPro.TMP_InputField passwordBox;
    private string email;
    private string password;
    #endregion

    #region gui operations

    public void modifyEmail()
    {
        email = emailBox.text;
    }
    public void modifyPassword()
    {
        password = passwordBox.text;
    }

    #endregion

    #region firebase operations

    void Start()
    {
        StartCoroutine(CheckDependencies());
    }

    IEnumerator CheckDependencies()
    {
        var checkTask = Firebase.FirebaseApp.CheckAndFixDependenciesAsync();
        yield return new WaitUntil(() => checkTask.IsCompleted);
        if (checkTask.Result == Firebase.DependencyStatus.Available)
        {
            app = Firebase.FirebaseApp.Create();
            InitializeFirebase();
        }
        else
        {
            Debug.LogError("Could not resolve all Firebase dependencies");
        }

    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);           
                gameUI.gameObject.SetActive(true);
                connection.gameObject.SetActive(false);
                pause.Restart();

            }
        }
    }



    void InitializeFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    #endregion

    #region connection

    public void attemptLogin()
    {

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            return;
        StartCoroutine(loginProcess());
    }

    IEnumerator loginProcess()
    {
        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => loginTask.IsCompleted);
        if (loginTask.IsCanceled)
        {
            //USER HAS CANCELED LOGIN
        }
        else if (loginTask.IsFaulted)
        {
            //SOME ERROR HAS HAPPENED
            Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + loginTask.Exception);
        }
    }

    public void attemptSignup()
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            return;
        StartCoroutine(signupProcess());
    }

    IEnumerator signupProcess()
    {
        var signupTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => signupTask.IsCompleted);
        if (signupTask.IsCanceled)
        {
            Debug.LogError("canceled");
        }
        else if (signupTask.IsFaulted)
        {
            Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + signupTask.Exception);
        }
    }

    public void Signout()
    {
        auth.SignOut();
        gameUI.gameObject.SetActive(false);
        connection.gameObject.SetActive(true);
    }
    #endregion
}

    

