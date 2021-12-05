import React from 'react';

import styled from 'styled-components';
import { MdKeyboardArrowRight, MdNoEncryption } from 'react-icons/md';
const Button = styled.a`
  display: flex;
  justify-content: center;
  align-items: center;
  border-radius: 18px;
  width: 65px;
  margin-left: 20.6em;
  height: 23px;
  margin-top: 5px;
  font-size: 10px;
  text-align: center;
  color: #fff;
  cursor: pointer;
  background: linear-gradient(90deg, #0546d6, #3f89fc);
  text-decoration: none;
  box-shadow: 0 15px 14px rgb(0 42 177 / 12%);
`;

const Tweetss = ({ posts, loading }) => {
  if (loading) {
    return <h2>Loading...</h2>;
  }

  return (
    <ul className='list-group mb-4'>
      {posts.map(post => (
        <p key={post.id} className='list-group-item'>
          {post.title}
          <Button  target='_blank'>
            <span>Analyze</span>
            <MdKeyboardArrowRight />
          </Button>
        </p>
      ))}
    </ul>
  );
};

export default Tweetss;