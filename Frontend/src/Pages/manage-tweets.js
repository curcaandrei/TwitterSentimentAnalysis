import React, { useState, useEffect } from 'react';
import BgImg from '../../src/tweet.png';
import ReactPaginate from "react-paginate";
import "./admin.css";
import axios from 'axios';
import styled from 'styled-components';
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

const ManageTweets= () => {

  
  var [pageNumber, setPageNumber] = useState(0);
  const [users, setUsers] = useState("");
  pageNumber++;


  useEffect(() => {
    axios("https://localhost:7225/api/Tweets/all/" + pageNumber,{
    }).then(res => setUsers(res.data));
  }, [pageNumber]);

  
  function deleteTweet(id){
    axios.delete("https://localhost:7225/api/Tweets/delete/" + id)
           .then(response =>{
             if(response!=null){
               console.log("done b");
              window.location.reload(false);
             }
           }
  
           );
    };

    
  const displayUsers = Object.keys(users).map((user,i) => {
    
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
            <a href="#" onClick={() => deleteTweet(users[user].id)} className="button-red">Delete</a>
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

export default ManageTweets;