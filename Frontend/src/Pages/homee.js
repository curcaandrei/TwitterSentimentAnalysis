import React, { useState, useEffect } from 'react';
import styled from 'styled-components';
import { MdKeyboardArrowRight, MdNoEncryption } from 'react-icons/md';
import BgImg from '../../src/tweet.png';
import JsonData from "../MOCK_DATA.json";
import ReactPaginate from "react-paginate";
import "../App.css";
import Navbar from '../Components/Navigation';
import { Link, Redirect, useHistory } from 'react-router-dom';
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

const Left = styled.div`
  padding-left: 220px;
  padding-top: 143px;
`;

const Title = styled.p`
  font-size: 2em;
  // margin-left: 8em;
  color: #04050a;
  font-weight: 400;
  text-align: -webkit-center;
`;
//descriere
const Desc = styled.p`
  width: 472px;
  font-size: 20px;
  color: #9ea0ac;
  line-height: 30px;
  margin-top: 58px;
`;

const Button = styled.a`
  display: flex;
  justify-content: center;
  align-items: center;
  border-radius: 18px;
  width: 95px;
  margin-left: 17.6em;
  height: 43px;
  margin-top: 5px;
  font-size: 17px;
  text-align: center;
  color: #fff;
  cursor: pointer;
  background: linear-gradient(90deg, #0546d6, #3f89fc);
  text-decoration: none;
  box-shadow: 0 15px 14px rgb(0 42 177 / 12%);
`;

const ButtonText = styled.a`
  display: flex;
  justify-content: center;
  align-items: center;
  border-radius: 18px;
  width: 95px;
  margin-left: 25.6em;
  margin-bottom: 0.3em;
  height: 43px;
  font-size: 17px;
  text-align: center;
  color: #fff;
  cursor: pointer;
  background: linear-gradient(90deg, #0546d6, #3f89fc);
  text-decoration: none;
  box-shadow: 0 15px 14px rgb(0 42 177 / 12%);
`;
const Homee = () => {
  
  const [users, setUsers] = useState(JsonData.slice(0, 1000));
  const [pageNumber, setPageNumber] = useState(0);

  const usersPerPage = 10;
  const pagesVisited = pageNumber * usersPerPage;

  var url = "";
  var tweet_id = "";
  const history = useHistory();
   let getDataAxios = async() => {
    let path = '/analyzer/' + tweet_id;
    history.push(path);
  }
  

  function submit_btn() {
    // https://twitter.com/Twitter/status/1466443318003384325
    if(url.includes('https://twitter.com/') && url.includes('/status/')){
      tweet_id = url.substring(url.indexOf('status/') + 7, url.length);
      getDataAxios();
    }
  }

  const displayUsers = users
    .slice(pagesVisited, pagesVisited + usersPerPage)
    .map((user) => {
      return (
        <div class="block-parent">
          <div class="tweet-list">
            <div class="tweet">
              <div class="twitter-icon">
                <div class="Icon Icon--twitter"></div>
              </div>
              <div class="tweet-author">
                <div class="TweetAuthor"><a class="TweetAuthor-link" href="#channel"> </a><span class="TweetAuthor-avatar"> 
                  <div class="Avatar"> </div></span><span class="TweetAuthor-name">{user.firstName}{user.lastName}</span> <span class="TweetAuthor-screenName">@{user.firstName}{user.lastName}</span></div>
                </div>
                <div class="tweet-text">BIG NEWS lol jk still Twitter</div>
                <div class="tweet-timestamp">
                  <span class="tweet-timestamp-date">Mon Apr 06 22:19:49 PDT 2009</span>
                </div>
              </div>
            </div>
            {/* <Link to={href} className="button">Analyze</Link> */}
            <a href={"/analyzer/" + user.id} id={user} class="button">Analyze</a>
          </div>
      );
    });

  const pageCount = Math.ceil(users.length / usersPerPage);

  const changePage = ({ selected }) => {
    setPageNumber(selected);
  };

  return (
    <Section>
      <Content>
       
        
          <div class="url" style={{color: "#04050a", fontweight: 400, textalign: "-webkit-center"}}>
          <input 
            type='text' 
            placeholder="Enter Here Tweet URL:" 
            id="tweetURL"
            onChange={(e) => {
              url = e.target.value;
              console.log(url);
            }} 
            style={{width: "36%", color: "#a3a3a3", font: "inherit", border: "groove", "border-radius": "1rem", padding: "12px 15px", margin: "2% 1% 0% 31%"}} />
          <button href="#"
             type="submit" 
             class="button2"
             onClick = {() => submit_btn()}
             >Analyze</button>
          </div>
       <div className="App">
      {displayUsers}
      <ReactPaginate
        previousLabel={"Previous"}
        nextLabel={"Next"}
        pageCount={pageCount}
        onPageChange={changePage}
        containerClassName={"paginationBttns"}
        previousLinkClassName={"previousBttn"}
        nextLinkClassName={"nextBttn"}
        disabledClassName={"paginationDisabled"}
        activeClassName={"paginationActive"}
      />
    </div>
        
      </Content>
    </Section>
  );
};

export default Homee;
