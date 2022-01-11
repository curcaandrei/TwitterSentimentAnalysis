import React, { useState, useEffect } from 'react';
import styled from 'styled-components';
import BgImg from '../../src/tweet.png';
import "./signin.css";
import axios from 'axios';
import LoadingScreen from 'react-loading-screen'
import jwt_decode from "jwt-decode";
import { useHistory } from 'react-router-dom';

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

const MLRequests = () => {
    var [myTweets, setJsonRes] = useState([]);
    var [loadingPage, setLoading] = useState(true);

    
    useEffect(() => {
        axios.get("https://localhost:7225/api/RequestTweet/all", {
            headers:{
            "Authorization": `Bearer ${localStorage.getItem('jwtToken')}`}
        }).then(function (res) {
            setJsonRes(res.data);
            setLoading(false);
        })
    }, [])

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
        var link = "https://localhost:7225/api/RequestTweet/accept-request/"
        link += myTweets[tweet].id;
      
            return (
        <div className="block-parent" key={i}>
          <div className="tweet-list" key={++i}>
            <div className="tweet" key={++i}>
              <div className="twitter-icon" key={++i}>
                <div className="Icon Icon--twitter" key={++i}></div>
              </div>
              <div className="tweet-author" key={++i}>
                <div className="TweetAuthor" key={++i}><a className="TweetAuthor-link" href="#channel"> </a><span className="TweetAuthor-avatar"> 
                  <div className="Avatar" key={++i}> </div></span><span className="TweetAuthor-name">{myTweets[tweet].user}</span> <span className="TweetAuthor-screenName">@{myTweets[tweet].username}</span></div>
                </div>
                <div className="tweet-text" key={++i}>{myTweets[tweet].text}</div>
                <div className="tweet-timestamp" key={++i}>
                  <span className="tweet-timestamp-date"></span>
                </div>
              </div>
            </div>
            <a id='info'></a>
            <a href="#" className="button-red" onClick={() => {axios.delete("https://localhost:7225/api/RequestTweet/delete/" + myTweets[tweet].id, {
        headers:{
        "Authorization": `Bearer ${localStorage.getItem('jwtToken')}`}
    })
    window.location.reload(false);
    }}>Deny</a>
            <a href="#" className="button-green" onClick={() => {axios.get(link, {
        headers:{
        "Authorization": `Bearer ${localStorage.getItem('jwtToken')}`}
    })
    document.getElementById("info").innerHTML = "This action will take a few minutes...";

    }}>Approve</a>
            
          </div>
      );
  })

  return (
    <Section>
      <Content>
       <div className="App">
      {displayTweets}
    </div>
        
      </Content>
    </Section>
  );
}

export default MLRequests;