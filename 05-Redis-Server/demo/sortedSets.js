const SortedSets = (redis) => {
    // set the original sorted sets
    redis.zadd('rockets', 1966, 'Luna 9', 1998, 'Deep Space 1', 1957, 'Sputnik', 1969, 'Apollo 11', 2008, 'Falcon 1');

    // print to console sorted set
    redis.zrangebyscore('rockets', '-inf', 1998, 'withscores', (err, result) => {
        console.log(result);
    });
}

export default SortedSets;
