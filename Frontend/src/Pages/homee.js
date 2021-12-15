import React, { useState, useEffect } from 'react';
import styled from 'styled-components';
import BgImg from '../../src/tweet.png';
import ReactPaginate from "react-paginate";
import "../App.css";
import { useHistory } from 'react-router-dom';
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


const Homee = () => {
  

  var [pageNumber, setPageNumber] = useState(0);

  const [users, setUsers] = useState("");
  // const [isLoading, setLoading] = useState(true);
  pageNumber++;


  useEffect(() => {
    axios("https://localhost:7225/api/Tweets/all/" + pageNumber,{
    }).then(res => setUsers(res.data));
    // setLoading(false);
  }, [pageNumber]);


  // const usersPerPage = 10;
  // const pagesVisited = pageNumber * usersPerPage;
  
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
  // console.log(users)
  const displayUsers = Object.keys(users).map((user,i) => {
    //  var good_id = users[user].id;
     //var good_id = new ObjectId(id).ObjectId;
      return (
        <div className="block-parent" key={i}>
          <div className="tweet-list" key={++i}>
            <div className="tweet" key={++i}>
              <div className="twitter-icon" key={++i}>
                <div className="Icon Icon--twitter" key={++i}></div>
              </div>
              <div className="tweet-author" key={++i}>
                <div className="TweetAuthor" key={++i}><a className="TweetAuthor-link" href="#channel"> </a><span className="TweetAuthor-avatar"> 
                  <div className="Avatar" key={++i}> </div></span><span className="TweetAuthor-name">{users[user].user}</span> <span className="TweetAuthor-screenName">@{users[user].user}</span></div>
                </div>
                <div className="tweet-text" key={++i}>{users[user].text}</div>
                <div className="tweet-timestamp" key={++i}>
                  <span className="tweet-timestamp-date" >{users[user].date}</span>
                </div>
              </div>
            </div>
            {/* <Link to={href} classNameName="button">Analyze</Link> */}
            <a href={"/analyzerdb/" + users[user].id} className="button">Analyze</a>
          </div>
      );
    });

  const pageCount = 104800;

  const changePage = ({ selected }) => {
    setPageNumber(selected);
  };

  return (
    <Section>
      <Content>
       
        
          <div className="url" style={{color: "#04050a", fontweight: 400, textalign: "-webkit-center"}}>
          <input 
            type='text' className="inputURL"
            placeholder="Enter Here Tweet URL:" 
            id="tweetURL"
            onChange={(e) => {
              url = e.target.value;
              // console.log(url);
            }} 
             />
          <a href="#"
             type="submit" 
             className="button2"
             onClick = {() => submit_btn()}
             >Analyze</a>
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
