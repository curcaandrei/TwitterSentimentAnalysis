import React, { useState, useEffect } from 'react';
import styled from 'styled-components';
import BgImg from '../../src/tweet.png';
import "./signin.css";
import axios from 'axios';
import LoadingScreen from 'react-loading-screen'

const Section = styled.section`
  background-image: url(${BgImg});
  
  height: 785px;
  display: block;
  background-repeat: no-repeat;
  background-size: contain;
`;

const Content = styled.div`
  width: 100%;
  height: 100px;
`;


const MyTweets2 = () => {
  

    const queryParams = new URLSearchParams(window.location.search);
    const tweetinvi_auth_request_id = queryParams.get('tweetinvi_auth_request_id');
    const oauth_token = queryParams.get('oauth_token');
    const oauth_verifier = queryParams.get('oauth_verifier');

  const [myTweets, setMyTweets] = useState([]);
  const [isLoading, setLoading] = useState(true);
  const [user, setUser] = useState([]);
  const [username, setUsername] = useState([]);

  useEffect(() => {
    axios.get("https://localhost:7225/signin", {
        params: {
            tweetinvi_auth_request_id: tweetinvi_auth_request_id,
            oauth_token : oauth_token,
            oauth_verifier: oauth_verifier
        }
    }).then( function (res) {
      setMyTweets(res.data.data);
      var myId;
    for(var prop in res.data.data[0]){
        if(prop === "id"){
          myId = res.data.data[0][prop];
        }
    }
    axios.get("https://localhost:7225/api/ExternalTwitter/tweetById/" + myId, {

      }).then(function (res2) {
        setUser(res2.data.user);
        setUsername(res2.data.username);
        setLoading(false);
      });
    });
  }, [oauth_token, oauth_verifier, tweetinvi_auth_request_id]);


    
  if(isLoading){
    return <LoadingScreen
    loading={true}
    bgColor='#f1f1f1'
    spinnerColor='#9ee5f8'
    textColor='#676767'
    text='Loading your tweets'
  />
  }

  const displayTweets = Object.keys(myTweets).map((tweet, i) =>{
      var good_id = myTweets[tweet].id;
      
            return (
        <div className="block-parent" key={i}>
          <div className="tweet-list" key={++i}>
            <div className="tweet" key={++i}>
              <div className="twitter-icon" key={++i}>
                <div className="Icon Icon--twitter" key={++i}></div>
              </div>
              <div className="tweet-author" key={++i}>
                <div className="TweetAuthor" key={++i}><a className="TweetAuthor-link" href="#channel"> </a><span className="TweetAuthor-avatar"> 
                  <div className="Avatar" key={++i}> </div></span><span className="TweetAuthor-name">{user}</span> <span className="TweetAuthor-screenName">@{username}</span></div>
                </div>
                <div className="tweet-text" key={++i}>{myTweets[tweet].text}</div>
                <div className="tweet-timestamp" key={++i}>
                  <span className="tweet-timestamp-date"></span>
                </div>
              </div>
            </div>
            <a href={"/analyzer/" + good_id} className="button">Analyze</a>
          </div>
      );
  })

  return (
    <Section>
      <Content>
       
        
          <div className="url" style={{color: "#04050a", fontweight: 400, textalign: "-webkit-center"}}>
          
          </div>
       <div className="App">
      {displayTweets}
    </div>
        
      </Content>
    </Section>
  );
};

export default MyTweets2;
