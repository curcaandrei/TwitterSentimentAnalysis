import React, { useState, useEffect } from 'react';
import styled from 'styled-components';
import BgImg from '../../src/tweet.png';
import "./signin.css";
import axios from 'axios';
import LoadingScreen from 'react-loading-screen'
import jwt_decode from "jwt-decode";

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

const MyTweets3 = () => {
    var [myTweets, setJsonRes] = useState([]);
    var [loadingPage, setLoading] = useState(true);
    var [username, setUsername] = useState(); 
    var userId = jwt_decode(localStorage.getItem('jwtToken')).userId
    
    useEffect(() => {
        axios.get("https://localhost:7225/api/Tweets/my-tweets/" + userId, {
            headers:{
            "Authorization": `Bearer ${localStorage.getItem('jwtToken')}`}
        }).then(function (res) {
            setJsonRes(res.data);
            setLoading(false);
            axios.get("https://localhost:7225/api/ExternalTwitter/tweetById/" + res.data[0].tweetId, {

            }).then(function (res2) {
              setUsername(res2.data.user);
            })
        })
    })

    if(loadingPage){
        return <LoadingScreen
        loading={true}
        bgColor='#f1f1f1'
        spinnerColor='#9ee5f8'
        textColor='#676767'
        text='Loading your tweets...'
      />
      }

    const displayTweets = Object.keys(myTweets).map((tweet, i) =>{
      var good_id = myTweets[tweet].tweetId;
      
            return (
        <div className="block-parent" key={i}>
          <div className="tweet-list" key={++i}>
            <div className="tweet" key={++i}>
              <div className="twitter-icon" key={++i}>
                <div className="Icon Icon--twitter" key={++i}></div>
              </div>
              <div className="tweet-author" key={++i}>
                <div className="TweetAuthor" key={++i}><a className="TweetAuthor-link" href="#channel"> </a><span className="TweetAuthor-avatar"> 
                  <div className="Avatar" key={++i}> </div></span><span className="TweetAuthor-name">{username}</span> <span className="TweetAuthor-screenName">{userId}</span></div>
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

}

export default MyTweets3;