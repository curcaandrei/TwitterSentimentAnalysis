import React, { useState, useEffect } from 'react';
import { PieChart, Pie, ResponsiveContainer } from "recharts";
import { useLocation } from "react-router";
import axios from 'axios';
import LoadingScreen from 'react-loading-screen'


function requestReview() {
  if(localStorage.getItem("jwtToken") == null) {
    return "";
  } else {
    return "Looking good? Help us improve the algorithm.";
  }
}


function requestbtn(_tweet) {
  document.getElementById("requestbtn").innerHTML = "Thank you for your support!";
  axios.post("https://localhost:7225/request-to-add",_tweet);
}

const Analyzer = () => {
  
  let location = useLocation();
  let url = location.pathname;
  let tweet_id = url.substring(10, url.length);
  var data2 = [];
  
  const [tweet, setTweet] = useState({hits: []});
  const [isLoading, setLoading] = useState(true);
  const [happy, setHappy] = useState("");
  const [sad, setSad] = useState("");
  const [data, setData] = useState([]);

  useEffect(() => {
    axios("https://localhost:7225/api/ExternalTwitter/tweetById/" + tweet_id,{
      
    }).then(function (res) {
      setTweet(res.data);
      for(const prop in res.data.feels){
        if(prop === "sad"){
          setSad(res.data.feels[prop]);
        }
        else if(prop === "happy"){
          setHappy(res.data.feels[prop]);
        }
      }
      setData([
        { name: "Happy", value: Number(parseFloat(happy*100).toFixed(2))  },
        { name: "Sad", value: Number(parseFloat(sad*100).toFixed(2)) },
      ]);
      setLoading(false);
    });
  }, []);

  for(var prop in data){
    if(data[prop].name === "Happy")
      data2.push({ name: "Happy", value: Number(parseFloat(happy*100).toFixed(2)), fill: '#bbe7d5' })
    else if(data[prop].name === "Sad")
      data2.push( { name: "Sad", value: Number(parseFloat(sad*100).toFixed(2)), fill: '#d8bfbb' })
  }

  if(isLoading){
    return <LoadingScreen
    loading={true}
    bgColor='#f1f1f1'
    spinnerColor='#9ee5f8'
    textColor='#676767'
    text='Analyzing your tweet'
    children=""
  />
  }

  return (
    <div>
      <div
        style={{
          display: "flex",
          justifyContent: "center",
          "marginTop": "4rem",
          "fontSize": "28px",
        }}
      >
        <h1>Statistics about this tweet</h1>
      </div>

      <div
        style={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          height: "68vh",
          "flexDirection": "column",
          "marginBottom": "5rem",
        }}
      >
        <div className="block-parent-analyzer">
          <div className="tweet-list">
            <div className="tweet">
              <div className="twitter-icon">
                <div className="Icon Icon--twitter"></div>
              </div>
              <div className="tweet-author">
                <div className="TweetAuthor">
                  <a className="TweetAuthor-link" href="#channel">
                    {" "}
                  </a>
                  <span className="TweetAuthor-avatar">
                    <div className="Avatar"> </div>
                  </span>
                  <span className="TweetAuthor-name">{tweet.user}</span>{" "}
                  <span className="TweetAuthor-screenName">@{tweet.username}</span>
                </div>
              </div>
              <div className="tweet-text">{tweet.text}</div>
              <div className="tweet-timestamp">
                <span className="tweet-timestamp-date">
                  {tweet.date}
                </span>
                <a href="#" type='submit' id="requestbtn" className="requestbtn" onClick = {() => requestbtn(tweet)}>{requestReview()}</a>
              </div>
            </div>
          </div>
        </div>

        <ResponsiveContainer width="30%" height={300}>
          <PieChart height={250}>
            <Pie
              data={data2}
              cx="50%"
              cy="50%"
              outerRadius={100}
              fill="green"
              dataKey="value"
              label={({
                cx,
                cy,
                midAngle,
                innerRadius,
                outerRadius,
                value,
                index,
              }) => {
                const RADIAN = Math.PI / 180;
                const radius = 25 + innerRadius + (outerRadius - innerRadius);
                const x = cx + radius * Math.cos(-midAngle * RADIAN);
                const y = cy + radius * Math.sin(-midAngle * RADIAN);

                return (
                  <text
                    x={x}
                    y={y}
                    fill="black"
                    textAnchor={x > cx ? "start" : "end"}
                    dominantBaseline="central"
                  >
                    {data2[index].name} ({value}%)
                  </text>
                );
              }}
            />
          </PieChart>
        </ResponsiveContainer>
      </div>
    </div>
  );
};

export default Analyzer;
