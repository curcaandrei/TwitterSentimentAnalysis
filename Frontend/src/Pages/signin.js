import React, { useState, useEffect } from 'react';
import styled from 'styled-components';
import BgImg from '../../src/tweet.png';
import "../App.css";
import axios from 'axios';


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

  useEffect(async () => {
    const result = await axios.get("https://localhost:7225/signin", {
        params: {
            tweetinvi_auth_request_id: tweetinvi_auth_request_id,
            oauth_token : oauth_token,
            oauth_verifier: oauth_verifier
        }
    });
    setMyTweets(result.data.data)
    setLoading(false);
  });
  if(isLoading){
    var myId = ""
    for(const prop in myTweets[0]){
        if(prop == "id"){
          myId = myTweets[0][prop]
        }
    }
  
    console.log(myId)
  }  
  


    useEffect(async () => {
        const result = await axios.get("https://localhost:7225/api/ExternalTwitter/tweetById/" + myId, {

        });
        setUser(result.data.user);
        setUsername(result.data.username);
        setLoading(false);
    });
  

  const displayTweets = Object.keys(myTweets).map((tweet, i) =>{
      var good_id = myTweets[tweet].id;
      
            return (
        <div class="block-parent">
          <div class="tweet-list">
            <div class="tweet">
              <div class="twitter-icon">
                <div class="Icon Icon--twitter"></div>
              </div>
              <div class="tweet-author">
                <div class="TweetAuthor"><a class="TweetAuthor-link" href="#channel"> </a><span class="TweetAuthor-avatar"> 
                  <div class="Avatar"> </div></span><span class="TweetAuthor-name">{user}</span> <span class="TweetAuthor-screenName">@{username}</span></div>
                </div>
                <div class="tweet-text">{myTweets[tweet].text}</div>
                <div class="tweet-timestamp">
                  <span class="tweet-timestamp-date"></span>
                </div>
              </div>
            </div>
            {/* <Link to={href} className="button">Analyze</Link> */}
            <a href={"/analyzer/" + good_id} class="button">Analyze</a>
          </div>
      );
  })

  return (
    <Section>
      <Content>
       
        
          <div class="url" style={{color: "#04050a", fontweight: 400, textalign: "-webkit-center"}}>
          
          </div>
       <div className="App">
      {displayTweets}
    </div>
        
      </Content>
    </Section>
  );
};

export default MyTweets2;
