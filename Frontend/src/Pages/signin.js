import React, { useState, useEffect } from 'react';
import "./signin.css";
import axios from 'axios';
import LoadingScreen from 'react-loading-screen'
import { useHistory } from 'react-router-dom';




const MyTweets2 = () => {
  

    const queryParams = new URLSearchParams(window.location.search);
    const tweetinvi_auth_request_id = queryParams.get('tweetinvi_auth_request_id');
    const oauth_token = queryParams.get('oauth_token');
    const oauth_verifier = queryParams.get('oauth_verifier');
    const history = useHistory();

  // const [myTweets, setMyTweets] = useState([]);
  // const [myTweets2, setMyTweets2] = useState([]);
  const [isLoading, setLoading] = useState(true);
  // const [user, setUser] = useState([]);
  // const [username, setUsername] = useState([]);
  // const [BearerToken, setBearerToken] = useState([]);
  // const [myData, setMyData] = useState([]);

  useEffect(() => {
    axios.get("https://localhost:7225/signin", {
        params: {
            tweetinvi_auth_request_id: tweetinvi_auth_request_id,
            oauth_token : oauth_token,
            oauth_verifier: oauth_verifier
        }
    }).then( function (res) {
      // setMyTweets(res.data.data);
      var myId;
    for(var prop in res.data.data[0]){
        if(prop === "id"){
          myId = res.data.data[0][prop];
        }
    }
    axios.get("https://localhost:7225/api/ExternalTwitter/tweetById/" + myId, {

      }).then(function (res2) {
        // setUser(res2.data.user);
        // setUsername(res2.data.username);
        localStorage.setItem('username', res2.data.username);
       
      
    var myData = []
  Object.keys(res.data.data).map((tweet) => {
    myData = ([...myData, {
      "id": res.data.data[tweet].id,
      "text": res.data.data[tweet].text
    }])
  });
  var jsonn = {
    "userId": "@".concat(res2.data.username),
    "data": myData
    }
    axios.post("https://localhost:7225/token/", jsonn).then(function (res3) {
      // setBearerToken(res3.data);
      console.log(res3.data);
      localStorage.setItem('jwtToken', res3.data);

    axios.get("https://localhost:7225/api/Tweets/my-tweets/" + "%40".concat(res2.data.username), {
      headers: {
        "Authorization": `Bearer ${localStorage.getItem('jwtToken')}`}
      }).then(function (res4) {
        console.log(res4.data);
        // setMyTweets2(res4.data);
        setLoading(false);
      });
    });
    });
  });
  }, [oauth_token, oauth_verifier, tweetinvi_auth_request_id]);

  if(isLoading){
    return <LoadingScreen
    loading={true}
    bgColor='#f1f1f1'
    spinnerColor='#9ee5f8'
    textColor='#676767'
    text='Signin in...'
  />
  }

    history.push("/");
    
    history.go(0);
  return (
    <div>
      
    </div>
  )

  // if(isLoading){
  //   return <LoadingScreen
  //   loading={true}
  //   bgColor='#f1f1f1'
  //   spinnerColor='#9ee5f8'
  //   textColor='#676767'
  //   text='Loading your tweets'
  // />
  // }

  
};

export default MyTweets2;
