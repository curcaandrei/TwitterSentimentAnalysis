import React, { useState, useEffect } from 'react';
import Navbar from "../Components/Navigation";
import { PieChart, Pie, ResponsiveContainer } from "recharts";
import { useLocation } from "react-router";
import axios from 'axios';

const Analyzer = () => {
  
  let location = useLocation();
  let url = location.pathname;
    let tweet_id = url.substring(10, url.length);
  
  const [tweet, setTweet] = useState({hits: []});
  const [isLoading, setLoading] = useState(true);
  const [feels, setFeels] = useState({hits: []});

  useEffect(async () => {
    const result = await axios("https://localhost:7225/api/ExternalTwitter/tweetById/" + tweet_id,{
      
    });
    setTweet(result.data);
    setFeels(result.data.feels);
    setLoading(false);
  }, []);

  console.log(tweet);

  // useEffect(async () => {
  //   const result = await axios({
  //     method: 'post',
  //     url: "https://localhost:7225/predictText",
  //     headers: {},
  //     data: {
  //       text: tweet.text
  //     }
  //   });
  //   setFeels(result.data);
  //   setLoading(false);
  // }, []);

  console.log(feels);



  const data = [
    { name: "Happy", value: parseFloat(feels.happy).toFixed(4) * 100},
    { name: "Sad", value: parseFloat(feels.sad).toFixed(4) * 100 }
  ];

  if(isLoading){
    return <div className="App4">Loading...</div>;
  }

  return (
    <div>
      <div
        style={{
          display: "flex",
          justifyContent: "center",
          "margin-top": "4rem",
          "font-size": "28px",
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
          "flex-direction": "column",
          "margin-bottom": "5rem",
        }}
      >
        <div class="block-parent-analyzer">
          <div class="tweet-list">
            <div class="tweet">
              <div class="twitter-icon">
                <div class="Icon Icon--twitter"></div>
              </div>
              <div class="tweet-author">
                <div class="TweetAuthor">
                  <a class="TweetAuthor-link" href="#channel">
                    {" "}
                  </a>
                  <span class="TweetAuthor-avatar">
                    <div class="Avatar"> </div>
                  </span>
                  <span class="TweetAuthor-name">{tweet.user}</span>{" "}
                  <span class="TweetAuthor-screenName">@{tweet.user}</span>
                </div>
              </div>
              <div class="tweet-text">{tweet.text}</div>
              <div class="tweet-timestamp">
                <span class="tweet-timestamp-date">
                  {tweet.date}
                </span>
              </div>
            </div>
          </div>
        </div>

        <ResponsiveContainer width="30%" height={300}>
          <PieChart height={250}>
            <Pie
              data={data}
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
                console.log("handling label?");
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
                    {data[index].name} ({value}%)
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
