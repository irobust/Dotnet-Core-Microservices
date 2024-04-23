const Hashes = (redis) => {
    redis.hmset('user:450', 'firstName', 'Jeremy', 'lastName', 'Henri');
    
    redis.hmset('user:450', 'address', 600);
    
    redis.hincrby('user:450', 'address', 300);
    
    redis.hgetall('user:450', (err, result) => {
        console.log(result);
    });
}

export default Hashes;
